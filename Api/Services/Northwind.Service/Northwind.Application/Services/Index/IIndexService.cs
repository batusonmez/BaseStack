namespace Northwind.Application.Services.Index
{
    public interface IIndexService
    {
       Task<IEnumerable<string>> SearchKeyword(string index, string keyword, int limit);
    }
}
