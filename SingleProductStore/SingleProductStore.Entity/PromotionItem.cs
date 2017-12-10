namespace SingleProductStore.Entity
{
    public class PromotionItem : BaseEntity
    {
        public int PromotionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
