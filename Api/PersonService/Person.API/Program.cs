using Dispatcher;
using EFAdapter;
using MassTransit;
using MediatR;
using MediatRDispatcher;
using Microsoft.EntityFrameworkCore;
using Person.API.Mapper;
using Person.Application.Maps;
using Person.Application.Models.Configuration;
using Person.Application.Services.Outbox;
using Person.Infrastructure.Services.Outbox;
using Person.Persistence;
using Repository;
using   Person.Infrastructure.CLI;
using System.CommandLine;
using Person.Infrastructure.CLI.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
   
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient(typeof(IDispatcher), typeof(MediatrDispatcher));
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Person.Application"));
builder.Services.AddAutoMapper(typeof(PersonAppProfile), typeof(PersonApiProfile));
builder.Services.Configure<IndexConfig>(builder.Configuration.GetSection("IndexConfig"));
builder.Services.AddScoped<IUOW, EFUnitOfWork>(sp =>
{
    var connString = configuration["ConnectionString"];
    if (string.IsNullOrEmpty(connString))
    {
        throw new NullReferenceException("Connection string is null");
    }
    var dbContextOptions = new DbContextOptionsBuilder<PersonAppContext>().UseSqlServer(connString).Options;
    var context = new PersonAppContext(dbContextOptions);
    return new EFUnitOfWork(context);
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped(typeof(IOutBoxService), typeof(OutboxService));

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

builder.Services.AddSingleton<OutboxIntegrationService>();
 
builder.Services.AddHostedService<OutboxIntegrationService>(provider =>
{
#pragma warning disable CS8603 // Possible null reference return.
    return provider.GetService<OutboxIntegrationService>();
#pragma warning restore CS8603 // Possible null reference return.

});

builder.Services.AddTransient<Command, IndexCommand>();
builder.Services.AddCLI(args);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
