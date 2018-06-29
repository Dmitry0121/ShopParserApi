using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopParserDataAccess.Entities
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public int Article { get; set; }

        [Required]
        public string Title { get; set; }
        public string Currency { get; set; }
        public string Characteristic { get; set; }
        public byte[] ImageArreyByte { get; set; }
        public ICollection<Price> ChangePrices { get; set; }
    }
}
