using Northwind.Application.Services.Index;

namespace Northwind.Infrastructure.Services.Index
{
    public class IndexService : IIndexService
    {
        public IEnumerable<string> SearchKeyword(string index, string keyword, int limit)
        {
            return new string[] { "1", "2", "3" };
        }
    }
}
