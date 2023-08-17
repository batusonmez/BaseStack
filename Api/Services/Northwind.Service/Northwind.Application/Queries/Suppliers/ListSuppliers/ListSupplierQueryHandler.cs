using AutoMapper;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Application.Services.Index;
using Northwind.Domain.Entities;
using Repository;
using System.Linq.Expressions;

namespace Northwind.Application.Queries.Suppliers.ListSuppliers
{
    public class ListSupplierQueryHandler : BasePagedQueryHandler<SupplierDTO, Supplier>
    {
        public ListSupplierQueryHandler(IMapper mapper, IRepository<Supplier> repository, IIndexService indexService) : base(mapper, repository, indexService)
        {
            
        }


        public override Expression<Func<Supplier, bool>>? BuildFilter(Query<SupplierDTO> request, IEnumerable<string>? indexSearchResult)
        {
            if (indexSearchResult != null)
            {
                IEnumerable<int> idlist = indexSearchResult.Select(d => int.Parse(d)).ToArray();
                return d => idlist.Contains(d.SupplierId);
            }

            return base.BuildFilter(request, indexSearchResult);
        }

    }
}
