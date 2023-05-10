namespace Repository
{
    public interface IUOW 
    {
        object Context { get; }
        Task Save();
    }
}
