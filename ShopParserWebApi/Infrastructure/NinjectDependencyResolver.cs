using Ninject;
using System;
using System.Collections.Generic;
using Ninject.Web.Common;
using System.Web.Http.Dependencies;
using ShopParserService.Interfaces;
using ShopParserService.Services;

namespace ShopParserWebApi.Infrastructure
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