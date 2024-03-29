using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => 
    builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("X-Current-Page", "X-Page-Size", "X-Total-Count", "X-Total-Pages")
    .AllowCredentials()
    .SetIsOriginAllowed((hosts) => true));
});
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
app.UseCors("CORSPolicy");
 

app.UseAuthorization();

app.MapControllers();
await app.UseOcelot();
app.Run();
