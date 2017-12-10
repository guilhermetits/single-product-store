using SingleProductStore.Entity.Enumarations;
using System.Collections.Generic;

namespace SingleProductStore.Entity
{
    public class Promotion : BaseEntity
    {
        public string Name { get; set; }
        public string Descrition { get; set; }
        public int? QuantityAvaliable { get; set; }
        public int IsBestOption { get; set; }
        public PromotionPricingType PromotionPricingType { get; set; }
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public bool CalculatedOriginalPrice { get; set; }
        public virtual ICollection<PromotionItem> PromotionItems { get; set; }
    }
}
