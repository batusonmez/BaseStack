using Dispatcher;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Queries.Token.GetToken;

namespace Northwind.API.Controllers.Token
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IDispatcher dispatcher;

        public TokenController(IDispatcher dispatcher
      ) 
        {
            this.dispatcher = dispatcher;
        }



        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {

            GetTokenQueryResponse? result = await dispatcher.Send<GetTokenQueryResponse>(new  GetTokenQuery());
            return Ok(result);
        }
    }
}
