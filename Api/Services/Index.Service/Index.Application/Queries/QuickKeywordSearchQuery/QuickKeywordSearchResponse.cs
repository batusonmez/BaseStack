using System.Collections;

namespace Index.Application.Queries.ListForKeys
{
    public class QuickKeywordSearchResponse:IEnumerable<string>
    {
       private IEnumerable<string> Keys { get; set; }
        public QuickKeywordSearchResponse()
        {
            Keys = new List<string>();
        }

        public IEnumerator<string> GetEnumerator()
        {
          return  Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Keys.GetEnumerator();
        }

        public void SetResult(IEnumerable<string> result)
        {
            Keys = result;
        }
    }
}
