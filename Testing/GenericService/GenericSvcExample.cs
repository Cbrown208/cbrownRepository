//using System;
//using System.IO;

//namespace GenericService
//{
//    public static class GenericSvcExample
//    {
//        private static string GetLoggerConfigurationFile()
//        {
//            var uri = new Uri(typeof(Program).Assembly.CodeBase);

//            var assemblyPath = uri.LocalPath;

//            var file = new FileInfo(assemblyPath);

//            var configFilePath = Path.Combine(String.Format("{0}", file.Directory), "cbo.AdHocRepricing.worker.log4net.config");

//            return configFilePath;
//        }

//        public static void Main(string[] args)
//        {
//            using (var container = StructureMapContainerFactory.Create<AdHocRepricingWorkerRegistry>())
//            {
//                var serviceName = AdHocRepricingWorkerServiceNameFactory.Create();

//                var configFilePath = GetLoggerConfigurationFile();

//                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFilePath));

//                Log4NetLogWriterFactory.Use(configFilePath);

//                AppDomain.CurrentDomain.UnhandledException += UnhandledException;

//                GenericSrv.TopshelfServiceHost.Run<AdHocRepricingWorkerService>(container, serviceName);
//            }
//        }

//        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
//        {
//            var logger = LogManager.GetLogger(typeof(AdHocRepricingWorkerService));

//            var exception = e.ExceptionObject as Exception;

//            if (exception != null)
//            {
//                logger.FatalFormat("Unhandled exception: {0}", exception.Message);
//                logger.FatalFormat("{0}", exception.StackTrace);
//            }
//            else if (e.ExceptionObject != null)
//            {
//                logger.FatalFormat("Unhandled Shananigans of type '{0}': '{1}'", e.ExceptionObject.GetType(), e.ExceptionObject);
//            }
//            else
//            {
//                logger.FatalFormat("Are you freaking kidding me??????");
//            }
//        }
//    }
//}
