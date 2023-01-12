using Dispatcher;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Person.API.Controllers;
using Person.Domain.Queries.ListPeople;

namespace Person.API.Person
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDispatcher dispatcher;

        public PersonController(ILogger<WeatherForecastController> logger, IDispatcher dispatcher)
        {
            _logger = logger;
            this.dispatcher = dispatcher;
        }



        [HttpGet(Name = "xxa")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
           var rsp=await dispatcher.Send<ListQueryResponse>(new ListPeopleQuery());
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}