using System.Collections.Generic;
using System.Linq;

namespace ShopParserApi.Models
{
    public class ProductViewModel
    {
        public ICollection<PriceViewModel> ChangePrices { get; set; }

        public int Id { get; set; }
        public int Article { get; set; }
        public string Title { get; set; }
        public decimal CurrentPrice
        {
            get
            {
                if (ChangePrices.Count > 0)
                {
                    var lastVal = (from v in ChangePrices
                                   let maxId = ChangePrices.Max(p => p.Id)
                                   where v.Id == maxId
                                   select v.ChangePrice).FirstOrDefault();

                    return lastVal;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string Currency { get; set; }
        public string Characteristic { get; set; }
        public string Path { get; set; }
        public byte[] Image { get; set; }
        public string Base64String { get; set; }
    }
}