using System.Net.Http;
using System.Threading.Tasks;
using YukonServiceMonitor.Clients.DataContracts;

namespace YukonServiceMonitor.Clients.Contracts
{
    public interface IServiceMonitor
    {
        ServiceWatchResponse GetServiceWatcherItems();
       
    }
}
