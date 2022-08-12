using System;
using System.Net;
using System.Net.Http;

namespace TlsTester
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

			// This site only accepts TLS 1.2
			var url = @"https://google.com/";

			try
			{
				Console.WriteLine("Testing TLS 1.2 Connection");
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(url);
					var httpResponseMessage = client.GetAsync("recaptcha/api/siteverify").Result;
					Console.WriteLine(httpResponseMessage.Content.ReadAsStringAsync().Result);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
				Console.WriteLine(ex.ToString());

			}
			Console.ReadLine();
		}
	}
}
