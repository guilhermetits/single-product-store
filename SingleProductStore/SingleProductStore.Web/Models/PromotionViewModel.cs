using System.Collections.Generic;

namespace SingleProductStore.Web.Models
{
    public class PromotionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrition { get; set; }
        public int? QuantityAvaliable { get; set; }
        public int IsBestOption { get; set; }
        public PromotionPricingType PricingType { get; set; }
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public bool CalculatedOriginalPrice { get; set; }
        public virtual ICollection<PromotionItemViewModel> PromotionItems { get; set; }

        public enum PromotionPricingType
        {
            None = 0,
            FixedPrice = 1,
            PercentualReduction = 2,
            FixedPriceReduction = 3,
        }
    }
}
