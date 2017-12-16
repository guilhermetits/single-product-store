using AutoMapper;
using SingleProductStore.Entity;
using SingleProductStore.Infrastructure;

namespace SingleProductStore.Web.Models.Mapper
{
    public class SpsMapperProfile : Profile
    {
        public SpsMapperProfile()
        {
            CreateMap<Promotion, PromotionViewModel>()
                .ForMember(pvm => pvm.PricingType, x => x.MapFrom(p => p.PromotionPricingType.ToInt()));
            CreateMap<PromotionItem, PromotionItemViewModel>();
            CreateMap<Product, ProductViewModel>();
        }
    }
}
