using System.Web.Http;
using NLog;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace DevStartPage.Web.AngularCli
{
    public class IoC
    {
        public static void Initialize()
        {

            ObjectFactory.Initialize(x => x.Scan(scanner =>
            {
                scanner.AssembliesFromApplicationBaseDirectory(
                    a => a.FullName.StartsWith("MedAssets") || a.FullName.StartsWith("PAS"));
                scanner.LookForRegistries();
                scanner.WithDefaultConventions();
                scanner.SingleImplementationsOfInterface();       
            }));


            var resolver = new DependencyResolver(ObjectFactory.Container);
            System.Web.Mvc.DependencyResolver.SetResolver(resolver);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }

    /// <summary>
    ///     Class for registration of assemblies
    /// </summary>
    public class ObjectRegistry : Registry
    {
        /// <summary>
        ///     Function to call to setup project.
        /// </summary>
        public ObjectRegistry()
        {
            For<ILogger>().Singleton().Use(LogManager.GetLogger("Default"));
        }
    }
}