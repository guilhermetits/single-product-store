using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingleProductStore.Entity;

namespace SingleProductStore.Data.Sql.Mapping
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.Price).HasColumnType("decimal(10,2)").IsRequired().HasDefaultValue(0m);
            builder.Property(x => x.Cost).HasColumnType("decimal(10,2)").IsRequired().HasDefaultValue(0m);
            builder.HasKey(x => x.Id).ForSqlServerIsClustered();
        }
    }
}
