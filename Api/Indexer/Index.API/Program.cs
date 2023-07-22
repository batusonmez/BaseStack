using Elasticsearch.Net;
using Index.Application.Common;
using Index.Application.Consumers;
using Index.Infrastructure;
using MassTransit;
using Nest;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<IndexDataConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(configuration["EventBusConnection"]);
        cfg.SendTopology.ConfigureErrorSettings = settings => settings.SetQueueArgument("x-message-ttl", 60000 * 60 * 24 * 2);
        cfg.ReceiveEndpoint("IndexDataQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(100, 10000));
            ep.ConfigureConsumer<IndexDataConsumer>(provider);

        });
    }));
});

SingleNodeConnectionPool pool = new(new(configuration["ElasticSearchUri"]));
ConnectionSettings settings = new(pool);
ElasticClient client = new(settings);
builder.Services.AddSingleton(client);
builder.Services.AddScoped(typeof(IIndexer), typeof(ESIndexer));
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
