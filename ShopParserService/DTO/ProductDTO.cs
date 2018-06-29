using System.Collections.Generic;

namespace ShopParserService.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int Article { get; set; }
        public string Title { get; set; }
        public decimal CurrentPrice { get; set; }
        public string Currency { get; set; }
        public string Characteristic { get; set; }
        public string Path { get; set; }
        public byte[] Image { get; set; }
        public string Base64String { get; set; }
        public ICollection<PriceDTO> ChangePrices { get; set; }
    }
}