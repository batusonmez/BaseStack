using Authentication.Application.Commands.Clients.CreateClient;
using Dispatcher;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IDispatcher dispatcher;

        public ClientController(IDispatcher dispatcher, IOpenIddictApplicationManager openIddictApplicationManager)  
        {
            this.dispatcher = dispatcher;
        }


        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateClientCommand data)
        {
            if(data == null)
            {
                return BadRequest();
            }

            CreateClientCommandResponse? result = await dispatcher.Send<CreateClientCommandResponse>(data);
            return Ok(result);
        }

    }
}
