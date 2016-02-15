using Magnum.PerformanceCounters;
using MedAssets.Common.Diagnostics;

namespace MonitoringService
{
    public class MonitorSetup
    {
        public class MonitoringPerformanceCounter : CounterCategory, IMonitorableCounterCategory
        {
            public Counter Requests_Succeeded { get; set; }

            public Counter Requests_Failed { get; set; }

            public Counter Requests_Response_Time { get; set; }
        }
        public class MedAssetsMonitoringTemplate : MonitoringPerformanceCounter
        { }
    }
}
