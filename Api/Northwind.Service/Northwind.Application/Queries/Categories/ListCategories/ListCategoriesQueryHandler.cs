using AutoMapper;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Queries.Categories.ListCategories
{
    public class ListCategoriesQueryHandler : QueryHandler<CategoryDTO, Category>
    {
        public ListCategoriesQueryHandler(IMapper mapper, IRepository<Category> repository) : base(mapper, repository)
        {
        }
        
    }
}
