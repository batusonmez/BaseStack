using Dispatcher;
using Elasticsearch.Net;
using Index.API.Services;
using Index.Application.Common;
using Index.Application.Consumers;
using Index.Infrastructure.ElasticSearch;
using MassTransit;
using MediatR;
using MediatRDispatcher;
using Nest;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();

#region Masstransit
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Masstransit
builder.Services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {                
                // if specified, waits until the bus is started before
                // returning from IHostedService.StartAsync
                // default is false
                options.WaitUntilStarted = true;

                // if specified, limits the wait time when starting the bus
                options.StartTimeout = TimeSpan.FromSeconds(30);

                // if specified, limits the wait time when stopping the bus
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<IndexDataConsumer>();
    x.UsingRabbitMq((context, cfg) =>
     {
         cfg.SendTopology.ConfigureErrorSettings = settings => settings.SetQueueArgument("x-message-ttl", 60000 * 60 * 24 * 2);
         cfg.ReceiveEndpoint("IndexDataQueue", ep =>
         {
             ep.PrefetchCount = 16;
             ep.UseMessageRetry(r => r.Interval(100, 10000));
             ep.ConfigureConsumer<IndexDataConsumer>(context);

         });
     });    
}); 

#endregion

#region MediaTR
builder.Services.AddTransient(typeof(IDispatcher), typeof(MediatrDispatcher));
builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Index.Application"));
#endregion

#region Elasticsearch
SingleNodeConnectionPool pool = new(new(configuration["ElasticSearchUri"]));
ConnectionSettings settings = new(pool);
ElasticClient client = new(settings);
builder.Services.AddSingleton(client);
builder.Services.AddScoped(typeof(IIndexer), typeof(ESIndexer));
#endregion

#region gRPC
builder.Services.AddGrpc();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGrpcService<IndexerService>();
app.MapControllers();

app.Run();
