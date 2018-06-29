using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopParserDataAccess.Entities
{
    [Table("Prices")]
    public class Price
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public decimal ChangePrice { get; set; }
        public DateTime DateChangePrice { get; set; }
    }
}