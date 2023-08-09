using Dispatcher;
using Northwind.Application.Models.DTO;

namespace Northwind.API.Controllers.Suppliers
{

    public class SupplierController : BaseAPIController<SupplierDTO>
    {
         
        public SupplierController(IDispatcher dispatcher
            ):base(dispatcher)
        { 
        }
         

    }
}
