using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Northwind.Infrastructure.BackgroundServices;
using Todo.Infrastructure.Logging;
namespace Todo.Infrastructure.BackgroundServices;

public abstract class BaseBackgroundService : BackgroundService, IBackgroundService
{
    private readonly int delay;
    private readonly ILogger<BaseBackgroundService> logger;

    public BaseBackgroundService(int delay, ILogger<BaseBackgroundService> logger)
    {
        this.delay = delay;
        this.logger = logger;
        if (delay < 1000)
        {
            throw new ArgumentException("Invalid delay time");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Execute();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            finally
            {
                await Task.Delay(delay, stoppingToken);
            }

        }
    }

    public abstract Task Execute();
}
