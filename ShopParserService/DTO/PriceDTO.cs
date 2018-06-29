using System;

namespace ShopParserService.DTO
{
    public class PriceDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal ChangePrice { get; set; }
        public DateTime DateChangePrice { get; set; }
    }
}
