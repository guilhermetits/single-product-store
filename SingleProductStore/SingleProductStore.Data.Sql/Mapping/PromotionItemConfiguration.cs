using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingleProductStore.Entity;

namespace SingleProductStore.Data.Sql.Mapping
{
    class PromotionItemConfiguration : IEntityTypeConfiguration<PromotionItem>
    {
        public void Configure(EntityTypeBuilder<PromotionItem> builder)
        {
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.Quantity).IsRequired().HasDefaultValue(1);
            builder.Property(x => x.ProductId).IsRequired();
            builder.HasKey(x => x.Id).ForSqlServerIsClustered();

            builder.HasAlternateKey(x => new { x.PromotionId, x.ProductId });
            builder.HasOne(x => x.Promotion)
                .WithMany(x => x.PromotionItems)
                .HasForeignKey(x => x.PromotionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
