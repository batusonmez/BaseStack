using Dispatcher;
using Northwind.Application.Models.DTO;

namespace Northwind.API.Controllers.Products
{

    public class CustomerController : BaseAPIController<CustomersDTO>
    {
         
        public CustomerController(IDispatcher dispatcher
            ):base(dispatcher)
        { 
        }
         

    }
}
