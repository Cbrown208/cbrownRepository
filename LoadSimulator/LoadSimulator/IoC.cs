using System;
using CommonServiceLocator.StructureMapAdapter.Unofficial;
using LoadSimulator.RabbitMQ;
using MassTransit;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Graph;

namespace LoadSimulator
{
	public class IoC
	{
		public static IContainer SettingsContainer;
		public static IContainer Container;
		public static IContainer Initialize()
		{
			var container = new Container(x => x.Scan(scanner =>
			{
				scanner.TheCallingAssembly();
				scanner.AssembliesFromApplicationBaseDirectory(a => a.FullName.StartsWith("MedAssets") || a.FullName.StartsWith("PAS", StringComparison.InvariantCultureIgnoreCase) || a.FullName.StartsWith("LoadSimulator", StringComparison.InvariantCultureIgnoreCase));
				scanner.WithDefaultConventions();
				scanner.SingleImplementationsOfInterface();
				scanner.LookForRegistries();
			}));
			ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator(container));
			container.Configure(
				x =>
				{
					var busSettings = BusSettings.FromAppConfig();
					x.ForSingletonOf<BusSettings>().Use(busSettings);
					var busDetails = new BusDetails();
					x.For<BusDetails>().Use(busDetails);

					x.ForSingletonOf<QueueManager>().Use<QueueManager>();

					x.For<IBusControl>().Use(busDetails.CreateBus(busSettings));
					x.For<IContainer>().Use(container);
				});
			Container = container;
			return Container;
		}

		public static T Get<T>()
		{
			var obj = Container.TryGetInstance<T>();
			if (obj == null)
			{
				obj = Container.GetInstance<T>();
			}
			return obj;
		}
	}
}
