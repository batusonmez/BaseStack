using Dispatcher;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;

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

            QueryResponse<T>? result = await Dispatcher.Send<QueryResponse<T>>(new Query<T>() { Page = page, PageSize = pageSize, QuickSearchKeyword = keyword });
            return Ok(result);
        }

    }
}
