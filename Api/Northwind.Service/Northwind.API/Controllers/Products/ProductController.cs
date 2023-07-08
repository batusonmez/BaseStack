using Dispatcher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;

namespace Northwind.API.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IDispatcher dispatcher;

        public ProductController(IDispatcher dispatcher
            )
        {
            this.dispatcher = dispatcher;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            
            QueryResponse<ProductsDTO>? result =  await dispatcher.Send<QueryResponse<ProductsDTO>>(new Query<ProductsDTO>() { Page = page, PageSize = pageSize });
            return Ok(result);
        }

    }
}
