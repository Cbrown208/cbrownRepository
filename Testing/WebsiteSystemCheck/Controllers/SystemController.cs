using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebsiteSystemCheck.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerInfo
    {
        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        public string ServerName { get; set; }
        /// <summary>
        /// Gets or sets the server time.
        /// </summary>
        /// <value>
        /// The server time.
        /// </value>
        public DateTime ServerTime { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SystemController : ApiController
    {
        // GET api/system
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public ServerInfo Get()
        {
            return new ServerInfo()
            {
                ServerName = Environment.MachineName,
                ServerTime = DateTime.UtcNow
            };
        }
    }
}