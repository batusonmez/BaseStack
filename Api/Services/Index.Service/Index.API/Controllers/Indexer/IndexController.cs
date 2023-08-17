using Dispatcher;
using Index.Application.Queries.ListForKeys;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Index.API.Controllers.Indexer
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly IDispatcher dispatcher;

        public IndexController(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }



        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] QuickKeywordSearchRequest request)
        {
             
            QuickKeywordSearchResponse? result= await dispatcher.Send<QuickKeywordSearchResponse>(request);
            
            return Ok(result);
        }
    }
}
