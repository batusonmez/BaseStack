namespace Dispatcher
{
    public interface IDispatcher
    {
        Task<object?> Send(object request, CancellationToken cancellationToken = default);
    }
}