namespace Index.Application.Models
{
    public class IndexQuery
    {
        public string IndexName { get; set; } = "";
        public string Query { get; set; } = "*";
        public int Limit { get; set; } = 10;
        public IndexQuery()
        {
            
        }
    }
}
