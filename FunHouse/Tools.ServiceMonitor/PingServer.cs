using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using NLog;
using Tools.Core.Models;

namespace Tools.ServiceMonitor
{
    public interface IPingServer
    {
        void PingAllServers(List<Servers> serverList);
        Servers Ping(Servers server);
        bool PingServersByEnv(List<Servers> serverList);
    }


    public class PingServer : IPingServer
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        //private readonly ServiceChecker _serviceChecker;

	    public async void PingAllServers(List<Servers> serverList)
        {
	        if (serverList == null)
	        {
		        return;
	        }

	        foreach (var server in serverList)
	        {
		        Ping(server);
	        }
        }

        public bool PingServersByEnv(List<Servers> serverList)
        {
            foreach (var server in serverList)
                Ping(server);
            return true;
        }

        public Servers Ping(Servers server)
        {
            var pingSender = new Ping();
            try
            {
                server.ServerName = server.ServerName.Trim();
                var reply = pingSender.Send(server.ServerName);
                if (reply != null && reply.Status == IPStatus.Success)
                {
                    server.Status = true;
                    server.Results = reply.Status.ToString();
                    server.Ip = reply.Address.ToString().Trim();
                    server.LastUpdated = DateTime.Now;
                }
                else
                {
                    server.Status = false;
                    //_serviceChecker.SetServiceStatusToDown(server.ServerName);
                }
            }
            catch (Exception ex)
            {
                server.Status = false;
                server.Results = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                _logger.Error(ex.Message);
            }
            return server;
        }
    }
}