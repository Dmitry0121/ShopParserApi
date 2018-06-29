using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopParserWebApi.Models
{
    public class PriceViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal ChangePrice { get; set; }
        public DateTime DateChangePrice { get; set; }
    }
}