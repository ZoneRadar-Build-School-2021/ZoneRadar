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
                    ToSpaceReviews = _reviewService.GetSpaceReview(),
                    TyoeOptions = _spaceService.GetTypeOption(),
                    CityOptions = _spaceService.GetCityOption()
                }
            };
            if(returnUrl != null)
            {
                ViewBag.IsLogin = true;
            }
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
        [HttpPost]
        public ActionResult SearchSpace(HomepageSearchViewModel homepageSearchVM)
        {
            _spaceService.SearchSpace(homepageSearchVM);
            throw new NotImplementedException();
        }
        public ActionResult SearchByCity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var space = _spaceService.GetSpaceByCity(id.Value);

            if (space.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(space);
        }
    }
}