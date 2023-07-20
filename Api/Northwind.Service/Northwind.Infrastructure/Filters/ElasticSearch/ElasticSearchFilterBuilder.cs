using Northwind.Application.Models.Filters;

namespace Northwind.Infrastructure.Filters.ElasticSearch
{
    public class ElasticSearchFilterBuilder : IFilterBuilder
    {
        public string Build(IEnumerable<Filter> filters)
        {
            return string.Empty;
        }
    }
}
