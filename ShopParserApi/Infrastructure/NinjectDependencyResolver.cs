using Ninject;
using Ninject.Web.Common;
using ShopParserService.Interfaces;
using ShopParserService.Services;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace ShopParserApi.Infrastructure
{
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam) : base(kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IProductService>().To<ProductService>().InRequestScope();
        }
    }
}