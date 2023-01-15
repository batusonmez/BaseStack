using Dispatcher;
using EFAdapter;
using MediatR;
using MediatRDispatcher;
using Microsoft.Extensions.DependencyInjection;
using Person.API.Mapper;
using Person.Domain.Entities;
using Person.Domain.Maps;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient(typeof(IDispatcher), typeof(MediatrDispatcher));
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Person.Domain"));
builder.Services.AddAutoMapper(typeof(PersonAppProfile), typeof(PersonApiProfile));

builder.Services.AddScoped<IUOW, EFUnitOfWork>(sp =>
{
    var context = new PersonAppContext( );
    return new EFUnitOfWork(context);
});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRePository<>));
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
