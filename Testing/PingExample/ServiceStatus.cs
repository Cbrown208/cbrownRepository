using System;
using System.Management;
using System.ServiceProcess;

namespace PingExample
{
    public class ServiceStatus
    {
        public static void GetServiceStatus(string svc)
        {
            // Toggle the Telnet service - 
            // If it is started (running, paused, etc), stop the service.
            // If it is stopped, start the service.
            ServiceController sc = new ServiceController("Telnet");
            Console.WriteLine("The Telnet service status is currently set to {0}",
                sc.Status.ToString());
            

            if ((sc.Status.Equals(ServiceControllerStatus.Stopped)) ||
                (sc.Status.Equals(ServiceControllerStatus.StopPending)))
            {
                // Start the service if the current status is stopped.

                Console.WriteLine("Starting the Telnet service...");
                sc.Start();
            }
            else
            {
                // Stop the service if its status is not set to "Stopped".

                Console.WriteLine("Stopping the Telnet service...");
                sc.Stop();
            }

            // Refresh and display the current service status.
            sc.Refresh();
            Console.WriteLine("The Telnet service status is now set to {0}.",
                sc.Status.ToString());

            //return GetServiceStatus();
        }

        public static string GetServiceStatus2()
        {
            ConnectionOptions op = new ConnectionOptions();
            op.Username = "MEDASSETS\\AIDevUser";
            op.Password = "armA1D3VM3d";
            ManagementScope scope = new ManagementScope(@"\\RCM41VQPASAPP01.MEDASSETS.com\root\cimv2", op);
            scope.Connect();
            ManagementPath path = new ManagementPath("Win32_Service");
            ManagementClass services;
            services = new ManagementClass(scope, path, null);
            string svcStatus = "Unknown";

            string EbMessageProcessorSvc = "EbMessageProcessorSvc";
            string AmsOpsNotificationSvc = "AmsOpsNotificationSvc";
            string AmsMonitorSvc = "AmsMonitorSvc";
            string EbBatchProcessor = "MedAssets.AMS.WinSvc.EbBatchProcessor";
            string ADTMessageProcessorSvc = "ADTMessageProcessorSvc";

            string ADTQueueListenerSvc = "ADTQueueListenerSvc";
            string WorkDistribution = "MedAssets.AMS.WinSvc.WorkDistribution";
            string ABNMessageProcessorSvc = "ABNMessageProcessorSvc";
            string EstimationMessageProcessor = "EstimationMessageProcessor";
            string IdentityVerificationProcessor = "IdentityVerificationProcessor";
            string P2PMessageProcessor = "P2PMessageProcessor";

            string temp = "";

            foreach (ManagementObject service in services.GetInstances())
            {
                
                //service.GetPropertyValue("Name").ToString().ToLower().Contains(svc);
                if (service.GetPropertyValue("Name").ToString().Contains(EbMessageProcessorSvc) || service.GetPropertyValue("Name").ToString().Contains(AmsOpsNotificationSvc) ||
                    service.GetPropertyValue("Name").ToString().Contains(AmsMonitorSvc) || service.GetPropertyValue("Name").ToString().Contains(EbBatchProcessor) ||
                    service.GetPropertyValue("Name").ToString().Contains(ADTMessageProcessorSvc) || service.GetPropertyValue("Name").ToString().Contains(ADTQueueListenerSvc) ||
                    service.GetPropertyValue("Name").ToString().Contains(WorkDistribution) || service.GetPropertyValue("Name").ToString().Contains(ABNMessageProcessorSvc) ||
                    service.GetPropertyValue("Name").ToString().Contains(EstimationMessageProcessor) || service.GetPropertyValue("Name").ToString().Contains(IdentityVerificationProcessor) ||
                    service.GetPropertyValue("Name").ToString().Contains(P2PMessageProcessor)
                    )
                {
                    temp ="  " + service.GetPropertyValue("Name");

                    svcStatus += temp+" is " + service.GetPropertyValue("State")+ Environment.NewLine;
                    

                    //if (service.GetPropertyValue("State").ToString().ToLower().Equals("running"))
                    //{
                    //   svcStatus = "Running";
                    //   return svcStatus;
                    //}
                    //if (service.GetPropertyValue("State").ToString().ToLower().Equals("stopped"))
                    //{
                    //    // Do something }
                    //    svcStatus = "Stopped";
                    //    return svcStatus;
                    //}
                }
            }
            return svcStatus;
            //return temp;
        }
    }
}