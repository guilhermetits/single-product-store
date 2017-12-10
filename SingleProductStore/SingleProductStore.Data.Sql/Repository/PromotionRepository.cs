using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Data.Sql.Repository.Contract;
using SingleProductStore.Entity;

namespace SingleProductStore.Data.Sql.Repository
{
    public class PromotionRepository : BaseRepository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(IDbContext context) : base(context)
        {
        }
    }
}
