using System.Data.Entity;

namespace ShopParserDataAccess.Entities.Context
{
    public class ShopParserContext : DbContext
    {
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product> Products { get; set; }
        public ShopParserContext() : base("ShopParser")
        {
        }
    }
}


                