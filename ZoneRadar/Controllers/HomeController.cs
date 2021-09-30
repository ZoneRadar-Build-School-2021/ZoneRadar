using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                SelectedSpaces = _spaceService.GetSelectedSpace(),
                ToSpaceReviews = _reviewService.GetSpaceReview(),
                TyoeOptions = _spaceService.GetTypeOption(),
                CityOptions = _spaceService.GetCityOption()
            };
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
            if (ModelState.IsValid)
            {
                return Content("成功接收");
            }

            return null;
        }
        public ActionResult AddTest()
        {
            //_service.TestMethod();

            return Content("新增完成");
        }
        //public ActionResult BookingInfo(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var space = _ser.GetSpace(id);

        //    if (space.Count() == 0)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(space);
        //}
    }
}