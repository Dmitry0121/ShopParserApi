using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using ShopParserDataAccess.Abstract;
using ShopParserDataAccess.Concrete;
using ShopParserDataAccess.Entities;
using ShopParserService.DTO;
using ShopParserService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace ShopParserService.Services
{
    public class ProductService : IProductService
    {
        IRepository _repository;
        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var productsDTO = new List<ProductDTO>();
            var products = _repository.GetAll("ChangePrices");
            foreach (var product in products)
            {
                var dto = new ProductDTO
                {
                    Id = product.Id,
                    Article = product.Article,
                    Title = product.Title,
                    Currency = product.Currency,
                    Characteristic = product.Characteristic,
                    Base64String = Convert.ToBase64String(product.ImageArreyByte),
                };

                if (product.ChangePrices.Count > 0)
                {
                    dto.CurrentPrice = product.ChangePrices.Max(p => p.ChangePrice);
                    dto.ChangePrices = product.ChangePrices.Select(p => new PriceDTO
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        ChangePrice = p.ChangePrice,
                        DateChangePrice = p.DateChangePrice
                    }).ToList();
                }                
                 productsDTO.Add(dto);
            }
            return productsDTO;
        }

        public ProductDTO GetProduct(int article)
        {
            var product = _repository.Get(p => p.Article == article, "ChangePrices");
            if (product != null)
            {
                var dto = new ProductDTO
                {
                    Id = product.Id,
                    Article = product.Article,
                    Title = product.Title,
                    Currency = product.Currency,
                    Characteristic = product.Characteristic,
                    Base64String = Convert.ToBase64String(product.ImageArreyByte),
                };

                if (product.ChangePrices.Count > 0)
                {
                    dto.CurrentPrice = product.ChangePrices.Max(p => p.ChangePrice);
                    dto.ChangePrices = product.ChangePrices.Select(p => new PriceDTO
                    {
                        Id = p.Id,
                        ProductId = p.ProductId,
                        ChangePrice = p.ChangePrice,
                        DateChangePrice = p.DateChangePrice
                    }).ToList();
                }
                return dto;
            }
            else
            {
                throw new Exception("Error object not found. Article = " + article);
            }            
        }

        public void ParseProducts(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    var htmlWeb = new HtmlWeb();
                    var document = htmlWeb.Load(url);
                    var page = document.DocumentNode;

                    foreach (var item in page.QuerySelectorAll("div.catalog-card-container"))
                    {
                        //#1
                        int article = 0;
                        var articleNode = item.QuerySelector(".product-code span:nth-child(2)");
                        if (articleNode != null)
                        {
                            int.TryParse(articleNode.InnerText, out article);
                        }
                        //#2
                        string imageUrl = "";
                        var imageUrlNode = item.QuerySelector(".image .product-img");
                        if (imageUrlNode != null)
                        {
                            imageUrl = imageUrlNode.GetAttributeValue("src", null);
                        }
                        //#3
                        string title = "";
                        var titleNode = item.QuerySelector(".title-itm h5");
                        if (titleNode != null)
                        {
                            title = titleNode.InnerText;
                        }
                        //#4
                        decimal currentPrice = 0;
                        var currentPriceNode = item.QuerySelector(".price-itm .base-price span:nth-child(1)");
                        if (currentPriceNode != null)
                        {
                            decimal.TryParse(Regex.Replace(currentPriceNode.InnerText, @"<[^>]+>|&nbsp;", "").Trim(), out currentPrice);
                        }
                        //#5
                        string currency = "";
                        var currencyNode = item.QuerySelector(".price-itm .base-price span:nth-child(2)");
                        if (currencyNode != null)
                        {
                            currency = currencyNode.InnerText;
                        }
                        //#6
                        string characteristic = "";
                        var characteristicNode = item.QuerySelector(".characteristic-short");
                        if (characteristicNode != null)
                        {
                            characteristic = characteristicNode.InnerText;
                        }

                        var parseProduct = new Product
                        {
                            Article = article,
                            Title = title,
                            Currency = currency,
                            Characteristic = characteristic,
                            ImageArreyByte = GetArreyByteByUrl(imageUrl)
                        };

                        var product = _repository.Get
                        (
                            p => p.Article == parseProduct.Article,                            
                            "ChangePrices"
                        );

                        if (product != null)
                        {
                            //check product
                            if (product.ChangePrices.Max(p => p.ChangePrice) != currentPrice)
                            {
                                _repository.CreatePrice(GetChangePriceForProduct(product, currentPrice));
                            }
                        }
                        else
                        {
                            //new product  
                            parseProduct.ChangePrices = new List<Price>();
                            _repository.CreateProduct(parseProduct);                            
                            var price = GetChangePriceForProduct(parseProduct, currentPrice);
                            if(parseProduct.Id != 0)
                                price.ProductId = parseProduct.Id;
                            _repository.CreatePrice(price);
                            
                        }
                    }
                }
                catch (Exception ex)
                {                    
                    throw new Exception("Error in parser logic. Message: " + ex.Message);
                }
            }
        }

        Price GetChangePriceForProduct(Product parseProduct, decimal price)
        {
            return new Price()
            {
                ProductId = parseProduct.Id,
                Product = parseProduct,
                ChangePrice = price,
                DateChangePrice = DateTime.Now
            };
        }

        byte[] GetArreyByteByUrl(string url)
        {
            try
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(url);
                return imageBytes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in convert a picture to an array of bytes. Message: " + ex.Message);
            }            
        }
    }
}