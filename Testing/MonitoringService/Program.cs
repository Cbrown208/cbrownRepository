using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Magnum.Cryptography.PKI;
using MedAssets.Common.Diagnostics;

namespace MonitoringService
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            // Manual Monitor 
            var AcountLog = new ManualMonitorLogMonitorActions();
            int Success = 1;
            int Fail = 0;

            DateTimeOffset requestTime = DateTimeOffset.Now;
            int i = 0;
            while (i <= 2)
            {
                AcountLog.MethodToUseLog(Success, requestTime);
                AcountLog.MethodToUseLog(Success, requestTime);
                AcountLog.MethodToUseLog(Success, requestTime);

                //System.Threading.Thread.Sleep(5000);

                AcountLog.MethodToUseLog(Fail, requestTime);
                AcountLog.MethodToUseLog(Fail, requestTime);
                AcountLog.MethodToUseLog(Fail, requestTime);
                i++;
            }

            // Montoring Thread
            ThreadMonitor tMonitor = new ThreadMonitor();
            tMonitor.MonitorThread();
            var message = "hello";

            //Monitor Action 
            Action action = () =>
            {
                ProcessMessage(message);
            };
            Monitoring<MonitorSetup.MonitoringPerformanceCounter>.Monitor(action);
            
        }

        public static void ProcessMessage(string message)
        {
            if (message != "hello")
            {
                message = "World";
            }
        }
    }
}


