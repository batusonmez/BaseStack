namespace Northwind.Application.Services.Index
{
    public interface IIndexService
    {
        IEnumerable<string> SearchKeyword(string index, string keyword, int limit);
    }
}
