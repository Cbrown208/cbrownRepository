using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace DevStartPage.Web.AngularCli
{
    public class DependencyResolver : System.Web.Mvc.IDependencyResolver, IDependencyResolver
    {
        private readonly IContainer _container;

        public DependencyResolver() : this(ObjectFactory.Container)
        {
        }

        public DependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            object result;

            if (serviceType.IsInterface || serviceType.IsAbstract)
            {
                result = _container.TryGetInstance(serviceType);
            }
            else
            {
                result = _container.GetInstance(serviceType);
            }

            return result;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public IDependencyScope BeginScope()
        {
            var container = _container.GetNestedContainer();
            return new DependencyResolver(container);
        }

        ~DependencyResolver()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeManagedResources();
            }

            DisposeNativeResources();
        }

        private void DisposeManagedResources()
        {
            _container.Dispose();
        }

        private void DisposeNativeResources()
        { }
    }
}