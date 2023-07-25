namespace Index.Application.Queries.ListForKeys
{
    public class QuickKeywordSearchResponse
    {
       public IEnumerable<string> Keys { get; set; }
        public QuickKeywordSearchResponse()
        {
            Keys = new List<string>();
        }
    }
}
