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
