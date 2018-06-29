using ShopParserService.DTO;
using System.Collections.Generic;

namespace ShopParserService.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAll();
        ProductDTO GetProduct(int productId);
        void ParseProducts(string url);
    }
}