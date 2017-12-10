using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Data.Sql.Repository.Contract;
using SingleProductStore.Entity;

namespace SingleProductStore.Data.Sql.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbContext context) : base(context)
        {
        }
    }
}
