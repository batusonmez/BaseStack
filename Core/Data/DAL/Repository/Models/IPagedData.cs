namespace Repository.Models
{
    public interface IPagedData<T> : IEnumerable<T> where T : class
    {
      
        int Total { get; set; }
    }
}
