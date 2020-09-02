﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using YukonServiceMonitor.Clients;
using YukonServiceMonitor.Clients.DataContracts;
using YukonServiceMonitor.Common;
using YukonServiceMonitor.Common.Contracts;

namespace YukonServiceMonitor
{
    class Program
    {
        static ILogger Logger;
        static readonly AppSettings AppSettings;

        static Program()
        {
            Logger = LoggingHelper.GetLogger<Program>();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettings = new AppSettings();

            configuration.GetSection("appSettings").Bind(AppSettings);
        }

        static void Main(string[] args)
        {
            StartAsync(args).GetAwaiter().GetResult();

            Console.ReadLine();
        }

        static async Task StartAsync(string[] args)
        {
            Logger.LogDebug("Starting service monitor...");

            var client = new Clients.YukonServiceMonitor();

            ServiceWatchResponse serviceWatcherItemsResponse = null;

            try
            {
                serviceWatcherItemsResponse = client.GetServiceWatcherItems();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error on retrieve watch items: {0}", ex);
                return;
            }

            foreach (var item in serviceWatcherItemsResponse.Model)
            {
                var watcherType = Type.GetType(item.TypeName, true);

                var watcherInstance = (IWatcher)Activator.CreateInstance(watcherType);

                await Task.Factory.StartNew(async () =>
                {
                    var controller = new MonitorService(Logger, watcherInstance, client, AppSettings);

                    await controller.ProcessAsync(item);
                });
            }
        }
    }
}
