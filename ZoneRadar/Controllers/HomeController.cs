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
            var model = new HomeViewModel
            {
                SelectedSpaces = _spaceService.GetSelectedSpace(),
                ToSpaceReviews = _reviewService.GetSpaceReviews(),
                TyoeOptions = _spaceService.GetTypeOptions(),
                CityOptions = _spaceService.GetCityOptions()
            };

            //使用者欲進入授權畫面但未登入的狀況(跳出登入Modal)
            if (Request.QueryString["ReturnUrl"] != null)
            {
                ViewBag.LoginModalPopup = true;
            }
            //嘗試登入失敗時的狀況(重新跳出登入Modal，並將原先輸入的Email顯示在欄位裡)
            if (TempData["Email"] != null)
            {
                //新增ModelState的Error訊息(顯示後就消不掉了)
                ModelState.AddModelError("LoginZONERadarVM.Password", "無效的帳號或密碼");
                ViewBag.LoginModalPopup = TempData["LoginModalPopup"];
                //model.LoginZONERadarVM = new LoginZONERadarViewModel { Email = (string)TempData["Email"] };
            }
            
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
        public void SearchSpace(HomepageSearchViewModel homepageSearchVM)
        {
            FormsIdentity id = (FormsIdentity)User.Identity;
            string memberId = User.Identity.Name;

            _spaceService.SearchSpacesByTypeCityDate(homepageSearchVM);
            throw new NotImplementedException();
        }
    }
}