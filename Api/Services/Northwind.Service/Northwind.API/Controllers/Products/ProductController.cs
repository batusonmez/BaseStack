using Dispatcher;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Commands;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries.ListQueryModels;
using Northwind.Application.Queries.Products.GetProduct;

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


        [HttpGet]
        [Route("ID/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(int id)
        {
            ProductsDTO? product =await Dispatcher.Send<ProductsDTO>(new GetProductQuery(id));
            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
