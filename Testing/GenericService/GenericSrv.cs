//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using GenericService.Model;
//using StructureMap;

//using Topshelf;
//using IContainer = StructureMap.IContainer;

//namespace GenericService
//{
//    internal class GenericSrv
//    {
//        public static class TopshelfServiceHost
//        {
//            public static Host Create<TService>(IContainer container, ServiceName serviceName)
//                where TService : class, IWindowsService
//            {
//                return Create<TService>(container, serviceName.Name, serviceName.DisplayName, serviceName.Description);
//            }

//            public static Host Create<TService>(IContainer container, string serviceName, string displayName,
//                string description) where TService : class, IWindowsService
//            {
//                var host = HostFactory.New(x =>
//                {
//                    x.Service<TService>(s =>
//                    {
//                        s.ConstructUsing(() => container.GetInstance<TService>());

//                        s.WhenStarted((service, hostControl) => service.Start(hostControl));
//                        s.WhenStopped((service, hostControl) => service.Stop(hostControl));
//                        s.WhenPaused((service, hostControl) => service.Pause(hostControl));
//                        s.WhenContinued((service, hostControl) => service.Continue(hostControl));
//                        s.WhenShutdown((service, hostControl) => service.Stop(hostControl));
//                    });

//                    x.EnablePauseAndContinue();

//                    x.EnableShutdown();

//                    x.RunAsLocalSystem();

//                    x.SetServiceName(serviceName);

//                    x.SetDisplayName(displayName);

//                    x.SetDescription(description);

//                    x.StartAutomaticallyDelayed();
//                });

//                return host;
//            }

//            public static TopshelfExitCode Run<TService>(IContainer container, ServiceName serviceName)
//                where TService : class, IWindowsService
//            {
//                return Run<TService>(container, serviceName.Name, serviceName.DisplayName, serviceName.Description);
//            }

//            public static TopshelfExitCode Run<TService>(IContainer container, string serviceName, string displayName,
//                string description) where TService : class, IWindowsService
//            {
//                var host = Create<TService>(container, serviceName, displayName, description);

//                var result = host.Run();

//                return result;
//            }
//        }
//    }
//}