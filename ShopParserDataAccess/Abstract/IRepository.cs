using ShopParserDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShopParserDataAccess.Abstract
{
    public interface IRepository
    {
        IEnumerable<Product> GetAll(string includeProperties = "");
        Product Get(Expression<Func<Product, bool>> filter = null, string includeProperties = "");
        void CreateProduct(Product item);
        void CreatePrice(Price item);
        void Update(Product item);
        void Delete(Product item);
    }
}
