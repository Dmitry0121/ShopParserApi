using Ninject.Modules;
using ShopParserService.Interfaces;
using ShopParserService.Services;

namespace ShopParserApi.Infrastructure
{
    public class NinjectModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>();
        }
    }
}