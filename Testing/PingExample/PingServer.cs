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
    class PingServer
    {
        string status;

        public static string SimplePing(string pingTarget)
        {
            string status;
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(pingTarget);
                if (reply.Status == IPStatus.Success)
                {
                    status = "Up";
                    Console.WriteLine("Address: {0}", reply.Address.ToString());
                    //Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                    //Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                    //Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                    //Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
                }

                else
                {
                    Console.WriteLine(reply.Status);
                    status = "Down";
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.InnerException.Message);
                status = "Down";
                return status;
            }
           
            return status;

        }
    }
}