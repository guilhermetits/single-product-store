using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SingleProductStore.Business.Contract.Service;
using SingleProductStore.Web.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SingleProductStore.Controllers
{
    public class HomeController : Controller
    {
        private IPromotionService promotionService;


        public HomeController(IPromotionService promotionService)
        {
            this.promotionService = promotionService;
        }

        public async Task<IActionResult> Index()
        {
            var promotions = await promotionService.GetAsync();
            promotions.Take(3);
            var promotionsViewModel = Mapper.Map<List<PromotionViewModel>>(promotions.ToList());
            return View(promotionsViewModel);
        }
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
