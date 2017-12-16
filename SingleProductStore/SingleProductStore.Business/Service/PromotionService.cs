using SingleProductStore.Business.Contract.Service;
using SingleProductStore.Data.Sql.Repository.Contract;
using SingleProductStore.Entity;

namespace SingleProductStore.Business.Service
{
    public class PromotionService : BaseService<Promotion>, IPromotionService
    {
        public PromotionService(IPromotionRepository repository) : base(repository)
        {
        }
    }
}
