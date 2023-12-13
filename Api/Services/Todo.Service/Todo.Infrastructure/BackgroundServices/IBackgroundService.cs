namespace Northwind.Infrastructure.BackgroundServices
{
    public interface IBackgroundService
    {
        Task Execute();
    }
}
