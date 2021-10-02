using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
        public ActionResult Register([Bind(Include = "RegisterZONERadarVM")] AllViewModel allVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            else
            {
                var registerVM = new RegisterZONERadarViewModel
                {
                    Name = allVM.RegisterZONERadarVM.Name,
                    Email = allVM.RegisterZONERadarVM.Email,
                    Password = allVM.RegisterZONERadarVM.Password,
                    ConfirmPassword = allVM.RegisterZONERadarVM.ConfirmPassword
                };
                var registerResult = _service.RegisterMember(registerVM);
                if (registerResult.IsSuccessful)
                {
                    var encryptedTicket = _service.CreateEncryptedTicket(registerResult.user);
                    _service.CreateCookie(encryptedTicket, Response);
                    return Redirect(_service.GetReturnUrl("qwe"));
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
            TempData["IsLogin"] = FormsAuthentication.IsEnabled;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "LoginZONERadarVM")] AllViewModel allVM)
        {
            //若未通過Model驗證
            if (!ModelState.IsValid)
            {
                return View();
            }
           
            var loginVM = new LoginZONERadarViewModel
            {
                Email = allVM.LoginZONERadarVM.Email,
                Password = allVM.LoginZONERadarVM.Password
            };

            var user = _service.UserLogin(loginVM);
                       
            //找不到則彈回Login頁
            if (user == null)
            {
                ModelState.AddModelError("Password", "無效的帳號或密碼");
                return View();
            }

            //建造加密表單驗證票證
            var encryptedTicket = _service.CreateEncryptedTicket(user);

            //建造cookie
            _service.CreateCookie(encryptedTicket, Response);

            //導向使用者原先欲造訪的路由
            return Redirect(_service.GetReturnUrl(user.MemberID.ToString()));
        }        

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}