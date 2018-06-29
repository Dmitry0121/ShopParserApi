using AutoMapper;
using ShopParserService.DTO;
using ShopParserService.Interfaces;
using ShopParserApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

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
            _productService = productService;
            _mapperProduct = new MapperConfiguration(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>()).CreateMapper();
            _mapperPrice = new MapperConfiguration(cfg => cfg.CreateMap<PriceDTO, PriceViewModel>()).CreateMapper();
        }

        public IEnumerable<ProductViewModel> Get()
        {
            var products = _productService.GetAll().ToList();
            var viewModel = _mapperProduct.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(products);
            return viewModel;
        }

        public ProductViewModel Get(int article)
        {
            var product = _productService.GetProduct(article);
            ProductViewModel viewModel = _mapperProduct.Map<ProductDTO, ProductViewModel>(product);
            viewModel.ChangePrices = _mapperPrice.Map<IEnumerable<PriceDTO>, List<PriceViewModel>>(product.ChangePrices);
            return viewModel;
        }

        [HttpGet]
        public void Parser(string url)
        {
            _productService.ParseProducts(url);
        }
    }
}
