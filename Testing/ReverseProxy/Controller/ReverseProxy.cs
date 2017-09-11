using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Diagnostics;

namespace ReverseProxy
{
    /// <summary>
    /// Handler all Client's requests and deliver the web site
    /// </summary>
    public class ReverseProxy : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// Method calls when client request the server
        /// </summary>
        /// <param name="context">HTTP context for client</param>
        public void ProcessRequest(HttpContext context)
        {
            // *************              ***********                **********
            // *           *   ------->   * Reverse *   --------->   * Remote *
            // * Navigator *              *  Proxy  *                * Server *
            // *           *   <-------   *         *   <---------   *        *
            // *************              ***********                **********

            // Create a connexion to the Remote Server to redirect all requests
            RemoteServer server = new RemoteServer(context);

            // Create a request with same data in navigator request
            HttpWebRequest request = server.GetRequest();

            // Send the request to the remote server and return the response
            HttpWebResponse response = server.GetResponse(request);
            byte[] responseData = server.GetResponseStreamBytes(response);

            // Send the response to client
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.ContentType = response.ContentType;
            context.Response.OutputStream.Write(responseData, 0, responseData.Length);

            // Handle cookies to navigator
            server.SetContextCookies(response);

            // Close streams            
            response.Close();
            context.Response.End();

        }

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        #endregion
    }
}
