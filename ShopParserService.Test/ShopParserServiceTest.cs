using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;
using ShopParserDataAccess.Abstract;
using ShopParserService.Services;
using ShopParserDataAccess.Entities;
using System.Linq;

namespace ShopParserService.Test
{
    [TestClass]
    public class ShopParserServiceTest
    {        
        [TestMethod]
        public void ReturnsResultWithAListOfProducts()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetAll("ChangePrices")).Returns(GetTestProducts());
            var _productService = new ProductService(mock.Object);

            // Act
            var result = _productService.GetAll();

            // Assert        
            Assert.AreEqual(GetTestProducts().Count, result.Count());
        }       

        [TestMethod]
        public void ReturnsProductWithPrices()
        {
            // Arrange      
            var mock = new Mock<IRepository>();
            int article = 99;
            mock.Setup(repo => repo.Get(p => p.Article == article, "ChangePrices"))
                .Returns(GetTestProducts().FirstOrDefault(p=>p.Article == article));

            mock.Setup(repo => repo.GetAll("ChangePrices")).Returns(GetTestProducts());
            var _productService = new ProductService(mock.Object);

            // Act
            var result = _productService.GetProduct(article);

            // Assert       
            var product = GetTestProducts().FirstOrDefault(p => p.Article == article);
            Assert.AreEqual(product.Title, result.Title);
            Assert.AreEqual(product.ChangePrices.Count, result.ChangePrices.Count());
        }
     
        [TestMethod]
        public void SendNullUrl()
        {
            // Arrange 
            var mock = new Mock<IRepository>();
            int countBefore = GetTestProducts().Count;
            var _productService = new ProductService(mock.Object);

            // Act
            _productService.ParseProducts("");

            // Assert       
            Assert.AreEqual(countBefore, GetTestProducts().Count);
        }

        private List<Product> GetTestProducts()
        {
            var products = new List<Product>
            {
                new Product { Id=1, Article=904, Title="iPhone 7", Currency="-", Characteristic="Apple", ImageArreyByte = new byte[10], ChangePrices = new List<Price>() },
                new Product { Id=2, Article=903, Title="Meizu 6+", Currency="-", Characteristic="Meizu", ImageArreyByte = new byte[10], ChangePrices = new List<Price>() },
                new Product { Id=3, Article=902, Title="Mi 5S+++", Currency="-", Characteristic="Xiaomi", ImageArreyByte = new byte[10], ChangePrices = new List<Price>() },
                new Product { Id=4, Article=901, Title="iPhone 7", Currency="-", Characteristic="Apple", ImageArreyByte = new byte[10], ChangePrices = new List<Price>() }
            };
            var product = new Product
            {
                Id = 5,
                Article = 99,
                Title = "iPhone 7",
                Currency = "-",
                Characteristic = "Apple",
                ImageArreyByte = new byte[10],
                ChangePrices = new List<Price>
                {
                    new Price { Id=1, ProductId=1, ChangePrice = 10, DateChangePrice = DateTime.Now },
                    new Price { Id=2, ProductId=1, ChangePrice = 10, DateChangePrice = DateTime.Now },
                    new Price { Id=3, ProductId=1, ChangePrice = 10, DateChangePrice = DateTime.Now },
                    new Price { Id=4, ProductId=1, ChangePrice = 10, DateChangePrice = DateTime.Now }
                }
            };
            products.Add(product);
            return products;
        }
    }
}