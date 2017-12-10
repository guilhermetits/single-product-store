using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingleProductStore.Entity;
using SingleProductStore.Entity.Enumarations;

namespace SingleProductStore.Data.Sql.Mapping
{
    class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Descrition);
            builder.Property(x => x.QuantityAvaliable);
            builder.HasKey(x => x.Id).ForSqlServerIsClustered();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.OriginalPrice);
            builder.Property(x => x.CalculatedOriginalPrice).IsRequired();
            builder.Property(x => x.PromotionPricingType).IsRequired().HasDefaultValue(PromotionPricingType.FixedPrice);
        }
    }
}
