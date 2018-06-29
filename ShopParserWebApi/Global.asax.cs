using Ninject;
using Ninject.Modules;
using Ninject.Web.WebApi;
using ShopParser.Util;
using ShopParserService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShopParserWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //NinjectModule orderModule = new NinjectModules();
            //NinjectModule serviceModule = new ServiceModule("ShopParserDb");
            //var kernel = new StandardKernel(orderModule, serviceModule);
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}