using Index.Application.Common;
using Index.Application.Queries.ListForKeys;
using MediatR;

namespace Index.Application.Queries.QuickKeywordSearchQuery
{
    public class QuickKeywordSearchHandler : IRequestHandler<QuickKeywordSearchRequest, QuickKeywordSearchResponse>
    {
        private readonly IIndexer indexer;

        public QuickKeywordSearchHandler(IIndexer indexer)
        {
            this.indexer = indexer;
        }
        public Task<QuickKeywordSearchResponse> Handle(QuickKeywordSearchRequest request, CancellationToken cancellationToken)
        {
           return Task.Run(() =>
            {
                QuickKeywordSearchResponse response = new();
                response.SetResult(indexer.QuickKeywordSearch(request));
                return response;
            });
        }
    }
}
