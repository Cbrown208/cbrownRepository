using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQApiCalls
{
	class Program
	{
		private static readonly RmqApiCallsManager Manager = new RmqApiCallsManager();
		static void Main(string[] args)
		{
			Manager.RunApiCallsTest();
		}
	}
}
