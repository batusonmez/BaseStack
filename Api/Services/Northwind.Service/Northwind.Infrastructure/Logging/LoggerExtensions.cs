using Microsoft.Extensions.Logging;

namespace Northwind.Infrastructure.Logging
{
    public static class LoggerExtensions
    {
        public static void LogException(this ILogger logger,Exception ex)
        {
            logger.LogError(ex.Message);
            if (ex.InnerException != null)
            {
                logger.LogError(ex.InnerException.Message);
            }

            
        }
    }
}
