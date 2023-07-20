namespace Northwind.Application.Models.Filters
{
    public interface IFilterBuilder
    {
        string Build(IEnumerable<Filter> filters);
    }
}
