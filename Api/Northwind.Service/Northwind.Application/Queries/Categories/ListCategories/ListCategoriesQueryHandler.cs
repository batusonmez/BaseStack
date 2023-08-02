using AutoMapper;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Application.Services.Index;
using Northwind.Domain.Entities;
using Repository;
using System.Linq.Expressions;

namespace Northwind.Application.Queries.Categories.ListCategories
{
    public class ListCategoriesQueryHandler : BasePagedQueryHandler<CategoryDTO, Category>
    {
        public ListCategoriesQueryHandler(IMapper mapper, IRepository<Category> repository, IIndexService indexService) : base(mapper, repository, indexService)
        {

        }

        public override Expression<Func<Category, bool>>? BuildFilter(Query<CategoryDTO> request, IEnumerable<string>? indexSearchResult)
        {
            if (indexSearchResult != null && indexSearchResult.Any())
            {
                IEnumerable<int> idlist = indexSearchResult.Select(d => int.Parse(d)).ToArray();
                return d => idlist.Contains(d.CategoryId);
            }

            return base.BuildFilter(request, indexSearchResult);
        }

    }
}
