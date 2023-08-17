using Dispatcher;
using Northwind.Application.Models.DTO;

namespace Northwind.API.Controllers.Products
{

    public class CategoryController : BaseAPIController<CategoryDTO>
    {
         
        public CategoryController(IDispatcher dispatcher
            ):base(dispatcher)
        { 
        }
         

    }
}
