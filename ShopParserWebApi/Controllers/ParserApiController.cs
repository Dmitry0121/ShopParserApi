using AutoMapper;
using ShopParserService.DTO;
using ShopParserService.Interfaces;
using ShopParserService.Services;
using ShopParserWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopParserWebApi.Controllers
{
    public class ParserApiController : ApiController
    {
        private IProductService _productService;
        IMapper _mapperProduct;
        IMapper _mapperPrice;

        public ParserApiController()//IProductService productService)
        {
            _productService = new ProductService(); //productService;
            _mapperProduct = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            _mapperPrice = new MapperConfiguration(cfg => cfg.CreateMap<PriceDTO, PriceViewModel>()).CreateMapper();
        }

        public IEnumerable<ProductViewModel> Get()
        {
            var products = _productService.GetAll().ToList();
            var viewModel = _mapperProduct.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(products);
            return viewModel;
           // return new List<ProductViewModel>();
        }

        public ProductViewModel Get(int article)
        {
            var product = _productService.GetProduct(article);
            ProductViewModel viewModel = _mapperProduct.Map<ProductDTO, ProductViewModel>(product);
            viewModel.ChangePrices = _mapperPrice.Map<IEnumerable<PriceDTO>, List<PriceViewModel>>(product.ChangePrices);
            return viewModel;
            //return new ProductViewModel();
        }

        [HttpGet]
        public void Parse(string url)
        {
            _productService.ParseProducts(url);
        }
    }
}
