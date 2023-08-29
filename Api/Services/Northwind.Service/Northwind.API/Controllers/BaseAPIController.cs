using Dispatcher;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Queries.GenericQueries.ListQueryModels;

namespace Northwind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseAPIController<T> : ControllerBase where T : class  
    {
        public readonly IDispatcher Dispatcher;

        public BaseAPIController(IDispatcher dispatcher)
        {
             
            this.Dispatcher = dispatcher;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? keyword)
        {

            ListQueryResponse<T>? result = await Dispatcher.Send<ListQueryResponse<T>>(new ListQuery<T>() { Page = page, PageSize = pageSize, QuickSearchKeyword = keyword });
            return Ok(result);
        }




    }
}
