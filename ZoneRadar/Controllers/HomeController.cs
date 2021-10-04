using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpaceService _spaceService;
        private readonly ReviewService _reviewService;
        public HomeController()
        {
            _spaceService = new SpaceService();
            _reviewService = new ReviewService();
        }

        public ActionResult Index(string returnUrl)
        {
            var model = new AllViewModel
            {
                HomeVM = new HomeViewModel
                {
                    SelectedSpaces = _spaceService.GetSelectedSpace(),
                    ToSpaceReviews = _reviewService.GetSpaceReviews(),
                    TyoeOptions = _spaceService.GetTypeOptions(),
                    CityOptions = _spaceService.GetCityOptions()
                }
            };
            if (returnUrl != null)
            {
                ViewBag.IsLogin = true;
            }
            //ViewBag.IsLogin = TempData["IsLogin"];
            //var model = new HomeViewModel
            //{
            //    SelectedSpaces = _spaceService.GetSelectedSpace(),
            //    ToSpaceReviews = _reviewService.GetSpaceReview(),
            //    TyoeOptions = _spaceService.GetTypeOption(),
            //    CityOptions = _spaceService.GetCityOption()
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

        /// <summary>
        /// 測試用的Action
        /// </summary>
        /// <param name="homepageSearchVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public void SearchSpace(HomepageSearchViewModel homepageSearchVM)
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            string memberId = User.Identity.Name;

            _spaceService.SearchSpacesByTypeCityDate(homepageSearchVM);
            throw new NotImplementedException();
        }
    }
}