using Dispatcher;
using EFAdapter;
using MediatR;
using MediatRDispatcher;
using Microsoft.Extensions.DependencyInjection;
using Person.API.Mapper;
using Person.Domain.Entities;
using Person.Domain.Maps;
using Person.Domain.Services.Outbox;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient(typeof(IDispatcher), typeof(MediatrDispatcher));
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Person.Domain"));
builder.Services.AddAutoMapper(typeof(PersonAppProfile), typeof(PersonApiProfile));

builder.Services.AddScoped<IUOW, EFUnitOfWork>(sp =>
{
    var connString = configuration["ConnectionString"];
    if (string.IsNullOrEmpty(connString))
    {
        throw new NullReferenceException("Connection string is null");
    }
    var context = new PersonAppContext(connString);
    return new EFUnitOfWork(context);
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRePository<>));
builder.Services.AddScoped(typeof(IOutBoxService), typeof(OutboxService));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
