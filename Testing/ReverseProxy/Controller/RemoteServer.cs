using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using System.Net;
using System.IO;

namespace ReverseProxy
{
    /// <summary>
    /// Manage communication between the proxy server and the remote server
    /// </summary>
    internal class RemoteServer
    {
        string _remoteUrl;
        HttpContext _context;

        /// <summary>
        /// Initialize the communication with the Remote Server
        /// </summary>
        /// <param name="context">Context</param>
        public RemoteServer(HttpContext context)
        {
            _context = context;

            // Convert the URL received from navigator to URL for server
            string serverUrl = ConfigurationSettings.AppSettings["RemoteWebSite"];
            _remoteUrl = context.Request.Url.AbsoluteUri.Replace("http://" + context.Request.Url.Host + context.Request.ApplicationPath, serverUrl);
        }

        /// <summary>
        /// Return address to communicate to the remote server
        /// </summary>
        public string RemoteUrl
        {
            get
            {
                return _remoteUrl;
            }
        }

        /// <summary>
        /// Create a request the remote server
        /// </summary>
        /// <returns>Request to send to the server</returns>
        public HttpWebRequest GetRequest()
        {
            CookieContainer cookieContainer = new CookieContainer();

            // Create a request to the server
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_remoteUrl);

            // Set some options
            request.Method = _context.Request.HttpMethod;
            request.UserAgent = _context.Request.UserAgent;
            request.KeepAlive = true;
            request.CookieContainer = cookieContainer;

            // Send Cookie extracted from the incoming request
            for (int i = 0; i < _context.Request.Cookies.Count; i++)
            {
                HttpCookie navigatorCookie = _context.Request.Cookies[i];
                Cookie c = new Cookie(navigatorCookie.Name, navigatorCookie.Value);
                c.Domain = new Uri(_remoteUrl).Host;
                c.Expires = navigatorCookie.Expires;
                c.HttpOnly = navigatorCookie.HttpOnly;
                c.Path = navigatorCookie.Path;
                c.Secure = navigatorCookie.Secure;
                cookieContainer.Add(c);
            }

            // For POST, write the post data extracted from the incoming request
            if (request.Method == "POST")
            {
                Stream clientStream = _context.Request.InputStream;
                byte[] clientPostData = new byte[_context.Request.InputStream.Length];
                clientStream.Read(clientPostData, 0, (int)_context.Request.InputStream.Length);

                request.ContentType = _context.Request.ContentType;
                request.ContentLength = clientPostData.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(clientPostData, 0, clientPostData.Length);
                stream.Close();
            }

            return request;

        }

        /// <summary>
        /// Send the request to the remote server and return the response
        /// </summary>
        /// <param name="request">Request to send to the server</param>
        /// <returns>Response received from the remote server or null if page not found</returns>
        public HttpWebResponse GetResponse(HttpWebRequest request)
        {
            HttpWebResponse response;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (System.Net.WebException)
            {
                // Send 404 to client 
                _context.Response.StatusCode = 404;
                _context.Response.StatusDescription = "Page Not Found";
                _context.Response.Write("<h1>Page not found</h1>");
                _context.Response.End();
                return null;
            }

            return response;
        }

        /// <summary>
        /// Return the response in bytes array format
        /// </summary>
        /// <param name="response">Response received from the remote server</param>
        /// <returns></returns>
        public byte[] GetResponseStreamBytes(HttpWebResponse response)
        {
            int bufferSize = 256;
            byte[] buffer = new byte[bufferSize];
            Stream responseStream;
            MemoryStream memoryStream = new MemoryStream();
            int remoteResponseCount;
            byte[] responseData;

            responseStream = response.GetResponseStream();
            remoteResponseCount = responseStream.Read(buffer, 0, bufferSize);

            while (remoteResponseCount > 0)
            {
                memoryStream.Write(buffer, 0, remoteResponseCount);
                remoteResponseCount = responseStream.Read(buffer, 0, bufferSize);
            }

            responseData = memoryStream.ToArray();

            memoryStream.Close();
            responseStream.Close();

            memoryStream.Dispose();
            responseStream.Dispose();

            return responseData;
        }

        /// <summary>
        /// Set cookies received from remote server to response of navigator
        /// </summary>
        /// <param name="response">Response received from the remote server</param>
        public void SetContextCookies(HttpWebResponse response)
        {
            _context.Response.Cookies.Clear();

            foreach (Cookie receivedCookie in response.Cookies)
            {
                HttpCookie c = new HttpCookie(receivedCookie.Name, receivedCookie.Value);
                c.Domain = _context.Request.Url.Host;
                c.Expires = receivedCookie.Expires;
                c.HttpOnly = receivedCookie.HttpOnly;
                c.Path = receivedCookie.Path;
                c.Secure = receivedCookie.Secure;
                _context.Response.Cookies.Add(c);
            }
        }
    }
}
