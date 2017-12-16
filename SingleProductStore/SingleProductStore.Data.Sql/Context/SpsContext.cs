using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SingleProductStore.Data.Sql.Mapping;

namespace SingleProductStore.Data.Sql.Context
{
    public class SpsContext : DbContext, IDbContext
    {

        public SpsContext(DbContextOptions<SpsContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionItemConfiguration());
            modelBuilder.ApplyConfiguration(new PromotionConfiguration());
        }
    }
}
