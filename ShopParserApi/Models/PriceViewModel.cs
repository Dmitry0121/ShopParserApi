using System;

namespace ShopParserApi.Models
{
    public class PriceViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal ChangePrice { get; set; }
        public DateTime DateChangePrice { get; set; }
    }
}