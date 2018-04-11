using System;

namespace LoadSimulator
{
	class Program
	{
		static void Main()
		{
			try
			{
				IoC.Initialize();
				var loadSimulatorSvc = new LoadSimulatorSvc();
				loadSimulatorSvc.Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
