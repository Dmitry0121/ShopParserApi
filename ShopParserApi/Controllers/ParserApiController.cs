using AutoMapper;
using ShopParserService.DTO;
using ShopParserService.Interfaces;
using ShopParserApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using ShopParserApi.ErrorLog;
using System;
using System.Net.Http;
using System.Net;

namespace ShopParserApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ParserApiController : ApiController
    {
        private IProductService _productService;
        IMapper _mapperProduct;
        IMapper _mapperPrice;

        public ParserApiController(IProductService productService)
        {
            if (productService == null)
            {
                NLogger.LogWrite().Error("Error IProductService");
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Server Error"),
                    ReasonPhrase = "Server Error"
                });
            }

            _productService = productService;
            _mapperProduct = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            _mapperPrice = new MapperConfiguration(cfg => cfg.CreateMap<PriceDTO, PriceViewModel>()).CreateMapper();
        }

        public IEnumerable<ProductViewModel> Get()
        {
            try
            {
                var products = _productService.GetAll().ToList();
                var viewModel = _mapperProduct.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(products);
                return viewModel;
            }
            catch (Exception ex)
            {
                NLogger.LogWrite().Error(ex, "Error ParserApi - get all products");
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Server Error"),
                    ReasonPhrase = "Server Error"
                });
            }
        }

        public ProductViewModel Get(int article)
        {
            if (article > 0)
            {
                try
                {
                    var product = _productService.GetProduct(article);
                    ProductViewModel viewModel = _mapperProduct.Map<ProductDTO, ProductViewModel>(product);
                    viewModel.ChangePrices = _mapperPrice.Map<IEnumerable<PriceDTO>, List<PriceViewModel>>(product.ChangePrices);
                    return viewModel;
                }
                catch (Exception ex)
                {
                    NLogger.LogWrite().Error(ex, "Error ParserApi - get details about product");
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Server Error"),
                        ReasonPhrase = "Server Error"
                    });
                }               
            }
            else
            {
                NLogger.LogWrite().Error("Error bad request. Article = " + article);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Error bad request. Article = " + article),
                    ReasonPhrase = "Error bad request. Article = " + article
                });
            }
        }

        [HttpGet]
        public void Parser(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    _productService.ParseProducts(url);
                }
                catch (Exception ex)
                {
                    NLogger.LogWrite().Error(ex, "Error ParserApi - parser");
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent("Server Error"),
                        ReasonPhrase = "Server Error"
                    });
                }                
            }
            else
            {
                NLogger.LogWrite().Error("Error bad request. Url = " + url);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Error bad request. Article = " + url),
                    ReasonPhrase = "Error bad request. Url = " + url
                });
            }
        }
    }
}
