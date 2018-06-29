using Ninject.Modules;
using ShopParserDataAccess.Abstract;
using ShopParserDataAccess.Concrete;

namespace ShopParserService.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IRepository>().To<Repository>().WithConstructorArgument(connectionString);
        }
    }
}