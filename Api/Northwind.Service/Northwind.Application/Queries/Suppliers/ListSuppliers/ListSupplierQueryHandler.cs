using AutoMapper;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Application.Services.Index;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Queries.Suppliers.ListSuppliers
{
    public class ListSupplierQueryHandler : BasePagedQueryHandler<SupplierDTO, Supplier>
    {
        public ListSupplierQueryHandler(IMapper mapper, IRepository<Supplier> repository, IIndexService indexService) : base(mapper, repository, indexService)
        {
            
        }
        
         
    }
}
