using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService;
        public HomeController()
        {
            _homeService = new HomeService();
        }

        public ActionResult Index(string returnUrl)
        {
            var model = new AllViewModel
            {
                HomeVM = new HomeViewModel
                {
                    SelectedSpaces = _homeService.GetSelectedSpace(),
                    ToSpaceReviews = _homeService.GetSpaceReview(),
                    TyoeOptions = _homeService.GetTypeOption(),
                    CityOptions = _homeService.GetCityOption()
                }
            };
            if(returnUrl != null)
            {
                ViewBag.IsLogin = true;
            }
            //var model = new HomeViewModel
            //{
            //    SelectedSpaces = _service.GetSelectedSpace(),
            //    ToSpaceReviews = _service.GetSpaceReview(),
            //    TyoeOptions = _service.GetTypeOption(),
            //    CityOptions = _service.GetCityOption()
            //};
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Policy()
        {
            return View();
        }

        [Authorize]
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult NotFound404()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult SearchSpace(HomepageSearchViewModel homepageSearchVM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _homeService.SearchSpace(homepageSearchVM);
        //        return Content("成功接收");
        //    }

        //    throw new NotImplementedException();
        //}
        public ActionResult SearchByCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var space = _homeService.GetSpaceByCity(id.Value);

            if (space.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(space);
        }
    }
}