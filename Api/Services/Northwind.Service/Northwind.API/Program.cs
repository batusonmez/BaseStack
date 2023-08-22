
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
using Microsoft.AspNetCore.Authentication;
using Northwind.API.Handlers.ExceptionManagement;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

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

//builder.Services.AddAuthentication().AddOAuth("oauth", o =>
//{
//    o.Events.OnTicketReceived
//});

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
builder.Services.AddMassTransit(d =>
{
    d.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(configuration["EventBusConnection"]);
        cfg.SendTopology.ConfigureErrorSettings = settings => settings.SetQueueArgument("x-message-ttl", 60000 * 60 * 24 * 2);
        cfg.ReceiveEndpoint("IndexDataQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(100, 10000));

        });
    }));
});
#endregion

#region CLI Commands
builder.Services.AddTransient<Command, ReindexCategoriesCommand>();
builder.Services.AddTransient<Command, ReindexSupplierCommand>();
builder.Services.AddCLI(args);
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

app.UseCors();
app.UseExceptionHandler(
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            //TODO implement log provider here
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "text/html";
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                var err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace}";
                                await context.Response.WriteAsync(err).ConfigureAwait(false);
                            }
                        });
                }
            );
//app.UseMiddleware<ExceptionHandler>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();