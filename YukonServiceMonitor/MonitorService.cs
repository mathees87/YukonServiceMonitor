using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YukonServiceMonitor.Clients.Contracts;
using YukonServiceMonitor.Clients.DataContracts;
using YukonServiceMonitor.Common;
using YukonServiceMonitor.Common.Contracts;

namespace YukonServiceMonitor
{
    public class MonitorService
    {
        public MonitorService(ILogger logger, IWatcher watcher, IServiceMonitor client, AppSettings appSettings)
        {
            Logger = logger;
            Watcher = watcher;
            Client = client;
            AppSettings = appSettings;
        }

        public ILogger Logger { get; }

        public IWatcher Watcher { get; }

        public IServiceMonitor Client { get; }

        public AppSettings AppSettings { get; }

        public async Task ProcessAsync(ServiceWatchItem item)
        {
            while (true)
            {
                try
                {
                    Logger?.LogTrace("{0} - Watching '{1}' for '{2}' environment", DateTime.Now, item.ServiceName, item.Environment);

                    var watchResponse = await Watcher.WatchAsync(new WatcherParameter(item.ToDictionary()));

                    if (watchResponse.Successful)
                        Logger?.LogInformation(" Success watch for '{0}' in '{1}' environment", item.ServiceName, item.Environment);
                    else
                        Logger?.LogError(" Failed watch for '{0}' in '{1}' environment", item.ServiceName, item.Environment);
                   
                }
                catch (Exception ex)
                {
                    Logger?.LogCritical(" Error watching service: '{0}': '{1}'", item.ServiceName, ex.Message);
                }

                Thread.Sleep(item.Interval ?? AppSettings.DelayTime);
            }
        }
    }
}
