
using Dispatcher;
using EFAdapter;
using MassTransit;
using MediatR;
using MediatRDispatcher;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Interceptors;
using Northwind.Application.Maps;
using Northwind.Application.Models.Configuration;
using Northwind.Application.Services.Index;
using Northwind.Application.Services.Outbox;
using Northwind.Infrastructure.CLI;
using Northwind.Infrastructure.CLI.Commands;
using Northwind.Infrastructure.Services.Index;
using Northwind.Infrastructure.Services.Outbox;
using Northwind.Persistence;
using Repository;
using System.CommandLine;
 using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Serilog.Events;
using Northwind.Application.Services.Token;
using Northwind.Infrastructure.Services.Token;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Northwind.API.Handlers.Policies;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes=true;
    
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddHttpContextAccessor();

#region Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.MetadataAddress = builder.Configuration["TokenConfig:MetaURL"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = builder.Configuration["TokenConfig:Audience"]
    };   
    options.Audience = builder.Configuration["TokenConfig:Audience"]; 
    options.Events = new()
    {
        OnTokenValidated = async context =>
        {
            if (context.Principal?.Identity is ClaimsIdentity claimsIdentity)
            {
                Claim? scopeClaim = claimsIdentity.FindFirst("scope");
                if (scopeClaim is not null)
                {
                    claimsIdentity.RemoveClaim(scopeClaim);
                    claimsIdentity.AddClaims(scopeClaim.Value.Split(" ").Select(s => new Claim("scope", s)).ToList());
                }
            }
            await Task.CompletedTask;
        }
    };
});
  
builder.Services.AddAuthorization(options => {
    options.AddPolicy(PolicyConst.READ, policy => policy.RequireClaim("scope", "read"));
    options.AddPolicy(PolicyConst.WRITE, policy => policy.RequireClaim("scope", "write"));        
});
#endregion

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region MediaTR
builder.Services.AddTransient(typeof(IDispatcher), typeof(MediatrDispatcher));
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Northwind.Application"));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PagingInterceptor<,>));
#endregion

#region Automapper
builder.Services.AddAutoMapper(typeof(NorthwindMapProfile), typeof(NorthwindMapProfile));
#endregion

#region ElasticSearch
builder.Services.Configure<IndexConfig>(builder.Configuration.GetSection("IndexConfig"));
builder.Services.AddScoped(typeof(IOutBoxService), typeof(OutboxService));
builder.Services.AddScoped(typeof(IIndexService), typeof(IndexService));
builder.Services.AddSingleton<OutboxIntegrationService>();
builder.Services.AddHostedService<OutboxIntegrationService>(provider =>
{
#pragma warning disable CS8603 // Possible null reference return.
    return provider.GetService<OutboxIntegrationService>();
#pragma warning restore CS8603 // Possible null reference return.

});
#endregion

#region Token
builder.Services.Configure<TokenConfig>(builder.Configuration.GetSection("TokenConfig"));
builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
#endregion

#region EntityFramework
builder.Services.AddScoped<IUOW, EFUnitOfWork>(sp =>
{
    var connString = configuration["ConnectionString"];
    if (string.IsNullOrEmpty(connString))
    {
        throw new NullReferenceException("Connection string is null");
    }
    var dbContextOptions = new DbContextOptionsBuilder<NorthwindContext>().UseSqlServer(connString).Options;
    var context = new NorthwindContext(dbContextOptions);
    return new EFUnitOfWork(context);
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
#endregion

#region MassTransit
builder.Services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {                                
                options.WaitUntilStarted = true;                                                
            });
builder.Services.AddMassTransit(d =>
{
    d.UsingRabbitMq((context, cfg) =>
    {
        cfg.AutoStart = true;    
        
        cfg.QueueExpiration=TimeSpan.FromDays(1);
        cfg.Host(configuration["EventBusConnection"]);
        cfg.SendTopology.ConfigureErrorSettings = settings => settings.SetQueueArgument("x-message-ttl", 60000 * 60 * 24 * 2);
        cfg.ReceiveEndpoint("IndexDataQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(100, 10000));

        });
    }); 
});

#endregion

#region CLI Commands
builder.Services.AddTransient<Command, ReindexCategoriesCommand>();
builder.Services.AddTransient<Command, ReindexSupplierCommand>();
builder.Services.AddCLI(args);
#endregion

#region Serilog
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration).Filter.ByIncludingOnly(evt => evt.Level != LogEventLevel.Information)
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors();
 

app.UseRouting();
app.UseHttpMetrics();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseMetricServer();
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapControllers();
    endpoints.MapMetrics();
});

//app.MapMetrics();
app.Run();
