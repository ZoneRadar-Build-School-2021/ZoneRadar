using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models;
using ZoneRadar.Services;
using ZoneRadar.ViewModels;

namespace ZoneRadar.Controllers
{
    public class MemberCenterController : Controller
    {
        private readonly ProfileService _qaz;
        public MemberCenterController()
        {
            _qaz = new ProfileService();
        }
        // GET: MemberCenter
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult EmailCheck()
        {
            return View();
        }
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            var userID = User.Identity.Name;
            var model = _qaz.GetProfileData(2);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "Photo,Name,Phone,Description")] ProfileViewModel p)
        {
            var model = _qaz.EditProfileData(p);
            return View(model);
        }
        
        public ActionResult MyCollection()
        {
            return View();
        }
        public ActionResult UserInfo()
        {
            return View();
        }
        public ActionResult HostInfo()
        {
            return View();
        }
        public ActionResult VerifyMail()
        {
            return View();
        }
    }
}