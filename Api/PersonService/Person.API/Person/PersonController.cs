using Dispatcher;
using MassTransit;
using MediatR;
using MessageBusDomainEvents;
using Microsoft.AspNetCore.Mvc;
using Person.API.Controllers;
using Person.API.Person.ListPople;
using Person.API.Person.NewPerson;
using Person.Domain.Commands.NewPerson;
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
        private readonly IPublishEndpoint eventBus;

        public PersonController(ILogger<WeatherForecastController> logger,
            IDispatcher dispatcher,
            IPublishEndpoint eventBus)
        {
            _logger = logger;
            this.dispatcher = dispatcher;
            this.eventBus = eventBus;
        }



        [HttpGet(Name = "xxa")]
        public async Task<ListPeopleAPIResponse> Get()
        {
            await eventBus.Publish(new IndexData());
            var response = new ListPeopleAPIResponse();
            var people = await dispatcher.Send<ListQueryResponse>(new ListPeopleQuery());
            if (people != null)
            {
                response.PersonList = people.People;
            }
            return response;
        }

        [HttpPut]
        public async Task<NewPersonAPIResponse> Put([FromBody] NewPersonAPIRequest person)
        {
            var response = new NewPersonAPIResponse();
            var inserted = await dispatcher.Send<NewPersonResponse>(new NewPersonCommand(person));
            if (inserted != null)
            {
                response.Person = inserted.Person;
            }
            return response;
        }
    }
}