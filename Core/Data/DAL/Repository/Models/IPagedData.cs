namespace Repository.Models
{
    public interface IPagedData<T> where T : class
    {
        IEnumerable<T> Data { get; set; }
        int Total { get; set; }
    }
}
