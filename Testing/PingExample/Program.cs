using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PingExample
{
    class Program
    {
        private readonly ServiceStatus _serviceStatus = new ServiceStatus();
        private static readonly WebsiteCheck _web = new WebsiteCheck();
        private static readonly WmiServerManager WmiManager = new WmiServerManager();

        static void Main(string[] args)
        {
            //string pingTarget = "RCM41VQPASAPP01";
            //var status = PingServer.SimplePing(pingTarget);
            //Console.WriteLine(status);


            //string svc = "MedAssets.AMS.WinSvc.EbMessageProcessor";
            //string svcResult = ServiceStatus.GetServiceStatus2();
            //Console.WriteLine(svcResult);

            //WmiManager.GetLastBootTime();
            //WmiManager.GetDiskspace();
            WmiManager.GetServerDiskSpace();


            //string url = "http://googleisawesome.com";
            //bool webresult = _web.PingWebsite(url);
            //Console.WriteLine(webresult);


            Console.ReadLine();
        }
    }
}
