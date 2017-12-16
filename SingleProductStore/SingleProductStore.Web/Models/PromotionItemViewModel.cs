namespace SingleProductStore.Web.Models
{
    public class PromotionItemViewModel
    {
        public int PromotionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual ProductViewModel Product { get; set; }
    }
}
