using SingleProductStore.Data.Sql.Context;
using SingleProductStore.Data.Sql.Repository.Contract;
using SingleProductStore.Entity;

namespace SingleProductStore.Data.Sql.Repository
{
    public class PromotionItemRepository : BaseRepository<PromotionItem>, IPromotionItemRepository
    {
        public PromotionItemRepository(IDbContext context) : base(context)
        {
        }
    }
}
