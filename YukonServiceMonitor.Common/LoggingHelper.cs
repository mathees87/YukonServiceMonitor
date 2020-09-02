using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace YukonServiceMonitor.Common
{
    public static class LoggingHelper
    {
        public static ILogger<TModel> GetLogger<TModel>()
        {
            var services = new ServiceCollection();

            services.AddLogging(logging =>
            {
                logging.AddConsole();
            }).Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug);

            return services
                .BuildServiceProvider()
                .GetService<ILoggerFactory>()
                .CreateLogger<TModel>();
        }
    }
}
