using Index.Application.Models;
using Index.Application.Queries.ListForKeys;

namespace Index.Application.Common
{
    public interface IIndexer
    {
        Task<bool> Index(string indexName, string id, object data);
        Task InitIndex(string indexName);        
        IEnumerable<string> QuickKeywordSearch(QuickKeywordSearchRequest query);
    }
}
