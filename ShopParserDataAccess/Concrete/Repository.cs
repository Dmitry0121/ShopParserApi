using ShopParserDataAccess.Abstract;
using ShopParserDataAccess.Entities;
using ShopParserDataAccess.Entities.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace ShopParserDataAccess.Concrete
{
    public class Repository : IRepository
    {
        ShopParserContext _context;
        public Repository(string connectionString)
        {
            _context = new ShopParserContext(connectionString);
        }

        public IEnumerable<Product> GetAll(string includeProperties = "")
        {
            IQueryable<Product> query = _context.Set<Product>();
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Product Get (Expression<Func<Product, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<Product> query = _context.Set<Product>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault();            
        }

        public void CreateProduct(Product item)
        {
            try
            {
                _context.Set<Product>().Add(item);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new DbEntityValidationException(e.Message);
                throw newException;
            }
        }

        public void Update(Product item)
        {
            try
            {
                _context.Set<Product>().Attach(item);
                _context.Entry(item).State = EntityState.Detached;
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new DbEntityValidationException(e.Message);
                throw newException;
            }
        }

        public void Delete(Product item)
        {
            try
            {
                _context.Set<Product>().Remove(item);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new DbEntityValidationException(e.Message);
                throw newException;
            }
        }

        public void CreatePrice(Price item)
        {
            try
            {
                _context.Set<Price>().Add(item);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new DbEntityValidationException(e.Message);
                throw newException;
            }
        }
    }
}
