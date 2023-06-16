namespace Repository
{
    public interface IUOW : IDisposable
    {
        object Context { get; }
        Task Save();
    }
}
