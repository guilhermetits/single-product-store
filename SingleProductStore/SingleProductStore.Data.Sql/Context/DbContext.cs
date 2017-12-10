using Microsoft.EntityFrameworkCore;
using SingleProductStore.Data.Sql.Mapping;

namespace SingleProductStore.Data.Sql.Context
{
    public class MobDbContext : DbContext, IDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionItemConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());
        }
    }
}
