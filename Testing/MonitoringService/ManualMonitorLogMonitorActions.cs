using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAssets.Common.Diagnostics;

namespace MonitoringService
{
    class ManualMonitorLogMonitorActions
    {
        public Action<TimeSpan> LogSuccess { get; set; }

        private void LogSuccessImpl(TimeSpan elapsed)
        {
            Monitoring<MonitorSetup.MedAssetsMonitoringTemplate>.Success(elapsed);
        }

        public Action<TimeSpan> LogFailure { get; set; }

        private void LogFailureImpl(TimeSpan elapsed)
        {
            Monitoring<MonitorSetup.MedAssetsMonitoringTemplate>.Failure(elapsed);
        }

        public Func<DateTime> GetCurrentDate { get; set; }

        private DateTime GetCurrentDateImpl()
        {
            return DateTime.Now;
        }

        public void MethodToUseLog( int result, DateTimeOffset rquestSentTime)
        {
            LogSuccess = LogSuccessImpl;
            LogFailure = LogFailureImpl;
            GetCurrentDate = GetCurrentDateImpl;
            var elapsed = GetCurrentDate() - rquestSentTime; 

            if (result == 1)
            {
                LogSuccess(elapsed);
            }
            else
            {
                LogFailure(elapsed);
            }
        }
    }
}
