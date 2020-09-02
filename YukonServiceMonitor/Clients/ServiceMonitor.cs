using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using YukonServiceMonitor.Clients.Contracts;
using YukonServiceMonitor.Clients.DataContracts;
using YukonServiceMonitor.Helpers;

namespace YukonServiceMonitor.Clients
{
    public class YukonServiceMonitor : IServiceMonitor
    {
       

        public YukonServiceMonitor()
        {            
        }

        public ServiceWatchResponse GetServiceWatcherItems() 
        {
            string jsonFilePath = Directory.GetCurrentDirectory().Split(new String[] { "bin" }, StringSplitOptions.None)[0] + "//SampleData.json";
           
            StreamReader sr = new StreamReader(jsonFilePath);
            var content = sr.ReadToEnd();          

            return SerializationHelper.Deserialze<ServiceWatchResponse>(content);
        }
    }
}
