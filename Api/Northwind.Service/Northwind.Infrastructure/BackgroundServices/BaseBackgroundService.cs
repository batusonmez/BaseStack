using Microsoft.Extensions.Hosting;

namespace Northwind.Infrastructure.BackgroundServices
{
    public abstract class BaseBackgroundService : BackgroundService, IBackgroundService
    {
        private readonly int delay;

        public BaseBackgroundService(int delay)
        {
            this.delay = delay;
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


                }
                finally
                {
                    await Task.Delay(delay, stoppingToken);
                }

            }
        }

        public abstract Task Execute();
    }
}
