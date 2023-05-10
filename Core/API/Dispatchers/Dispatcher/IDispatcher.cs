namespace Dispatcher
{

    public interface IDispatcher
    {

        Task<object?> Send(object request, CancellationToken cancellationToken = default);

        Task<T?> Send<T>(object request, CancellationToken cancellationToken = default) where T : class;

    }
}