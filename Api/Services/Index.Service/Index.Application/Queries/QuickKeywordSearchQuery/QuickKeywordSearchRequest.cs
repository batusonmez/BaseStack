using MediatR;

namespace Index.Application.Queries.ListForKeys
{

    public class QuickKeywordSearchRequest : IRequest<QuickKeywordSearchResponse>
    {
        public string IndexName { get; set; } = "";
        public string Query { get; set; } = "*";
        public int Limit { get; set; } = 10;

    }
}
