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
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddControllers();

#region Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Masstransit
builder.Services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {                                
                options.WaitUntilStarted = true;                            
            });
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<IndexDataConsumer>();
    x.UsingRabbitMq((context, cfg) =>
     {
         cfg.AutoStart = true;                       
         cfg.QueueExpiration = TimeSpan.FromDays(1);
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

#region Serilog
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration).Filter.ByIncludingOnly(evt => evt.Level != LogEventLevel.Information)
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
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
