using Dispatcher;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Commands;
using Northwind.Application.Models.DTO;

namespace Northwind.API.Controllers.Products
{

    public class ProductController : BaseAPIController<ProductsDTO>
    {        
        public ProductController(IDispatcher dispatcher
            ):base(dispatcher)
        {            
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] ProductsDTO data)
        {

            UpsertCommandResponse? result = await Dispatcher.Send<UpsertCommandResponse>(new UpsertCommand<ProductsDTO>(data));
            return Ok(result);
        }
    }
}
