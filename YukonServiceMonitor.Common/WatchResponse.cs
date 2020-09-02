using YukonServiceMonitor.Common.Contracts;

namespace YukonServiceMonitor.Common
{
    public class WatchResponse : IWatchResponse
    {
        public bool Successful { get; set; }

        public string ShortMessage { get; set; }

        public string FullMessage { get; set; }
    }
}
