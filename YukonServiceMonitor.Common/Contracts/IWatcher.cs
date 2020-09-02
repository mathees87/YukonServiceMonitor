using System.Threading.Tasks;

namespace YukonServiceMonitor.Common.Contracts
{
    public interface IWatcher
    {
        string ActionName { get; }

        Task<WatchResponse> WatchAsync(WatcherParameter parameter);
    }
}
