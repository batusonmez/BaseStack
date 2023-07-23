using Index.Application.Models;

namespace Index.Application.Common
{
    public interface IIndexer
    {
        Task<bool> Index(string indexName, string id, object data);
        Task InitIndex(string indexName);        
        IEnumerable<string> QueryForKeys(IndexQuery query);
    }
}
