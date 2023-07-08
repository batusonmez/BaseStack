namespace Dispatcher
{
    public interface IQueryResult 
    {
        int Total { get; set; }
        int PageSize { get; set; }
        int Page { get; set; }
        int TotalPages { get; set; }
    }
}
