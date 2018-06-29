using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ShopParserDataAccess.Entities.Context
{
    public class ShopParserContext : DbContext
    {
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product> Products { get; set; }

        static ShopParserContext()
        {
            Database.SetInitializer<ShopParserContext>(new StoreDbInitializer());
        }

        public ShopParserContext(string connectionString) : base(connectionString)
        {
        }
    }

    public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<ShopParserContext>
    {
        protected override void Seed(ShopParserContext db)
        {
            db.Products.Add
                (
                    new Product
                    {
                        Article = 99,
                        Title = "iPhone 7",
                        Currency = "uh",
                        Characteristic = "Apple",
                        ImageArreyByte = new byte[10],
                        ChangePrices = new List<Price>
                        {
                            new Price { Id=1, ProductId=1, ChangePrice = 1000, DateChangePrice = DateTime.Now.AddDays(-1) },
                            new Price { Id=2, ProductId=1, ChangePrice = 1001, DateChangePrice = DateTime.Now.AddDays(-2) },
                            new Price { Id=3, ProductId=1, ChangePrice = 1010, DateChangePrice = DateTime.Now.AddDays(-3) },
                            new Price { Id=4, ProductId=1, ChangePrice = 1100, DateChangePrice = DateTime.Now.AddDays(-4) }
                        }
                    }
                );
            db.SaveChanges();
        }
    }
}