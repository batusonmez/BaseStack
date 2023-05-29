using Dispatcher;
using MassTransit;
using MediatR;
using MessageBusDomainEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Person.API.Controllers;
using Person.API.Person.ListPople;
using Person.API.Person.NewPerson;
using Person.Application.Commands.NewPerson;
using Person.Application.Queries.ListPeople;

namespace Person.API.Person
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IDispatcher dispatcher;

        public PersonController(IDispatcher dispatcher
            )
        {
            this.dispatcher = dispatcher;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery]int pageSize)
        {
            var result = await dispatcher.Send<ListPeopleQueryResponse>(new ListPeopleQuery() { Page = page, PageSize = pageSize }) ;
            return Ok(result);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] NewPersonAPIRequest person)
        {
            if(person == null)
            {
                return BadRequest();
            }
            var response = new NewPersonAPIResponse();
            var inserted = await dispatcher.Send<NewPersonResponse>(new NewPersonCommand(person));
            if (inserted != null)
            {
                response.Person = inserted.Person;
            }
            return Ok(response);
        }


        [HttpPut]
        [Route("ReIndex")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<IActionResult> ReIndexAll()
        {
                        
             await dispatcher.Send<ReIndexPeopleResponse>(new ReIndexPeopleCommand());

            return Ok();
        }
    }
}