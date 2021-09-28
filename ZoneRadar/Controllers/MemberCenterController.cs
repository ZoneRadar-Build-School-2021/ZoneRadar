using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class MemberCenterController : Controller
    {
        private readonly MemberService _service;
        public MemberCenterController()
        {
            _service = new MemberService();
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
        public ActionResult NewPassword()
        {
            return View();
        }
        public ActionResult EditProfile()
        {
            return View();
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


        [HttpGet]
        public ActionResult Register()
        {
            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string email, string password, string name)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            else
            {
                var registerVM = new RegisterZONERadarViewModel
                {
                    Email = email,
                    Password = password,
                    Name = name
                };
                var isSuccessful = _service.RegisterMember(registerVM);
                if (isSuccessful)
                {
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    return View("Register");
                }
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            return RedirectToAction("index", "Home");
        }
    }
}