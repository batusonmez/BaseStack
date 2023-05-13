namespace Dispatcher
{
    public interface IQueryResult<T> : IEnumerable<T> where T : class
    {
        int Total { get; set; }

    }
}
