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
                    var encryptedTicket = _service.CreateEncryptedTicket(registerResult.User);
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
            //(每個非授權畫面都要加這行，例：預約頁面)
            TempData["LoginModalPopup"] = true;
            //如果是想進入未授權畫面而跳出登入Modal
            if (Request.QueryString["ReturnUrl"] != null)
            {
                return Redirect($"{Request.UrlReferrer.AbsolutePath}?ReturnUrl={Request.QueryString["ReturnUrl"]}");
            }
            //因Login Modal是JS控制跳出，所以一般登入步驟不會進這行
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "LoginZONERadarVM")] AllViewModel allVM)
        {
            //若未通過Model驗證(前端已先驗證過)
            if (!ModelState.IsValid)
            {
                //回到原本頁面並跳出登入Modal
                TempData["LoginModalPopup"] = true;
                Response.Redirect(Request.UrlReferrer.AbsolutePath);
                //return View();
            }
           
            var loginVM = new LoginZONERadarViewModel
            {
                Email = allVM.LoginZONERadarVM.Email,
                Password = allVM.LoginZONERadarVM.Password
            };

            var user = _service.UserLogin(loginVM);
                       
            //找不到則彈回Login頁(問題：不會跳出錯誤訊息)
            if (user == null)
            {
                var xxx = ModelState["LoginZONERadarVM.Password"];
                TempData["LoginModalPopup"] = true;
                TempData["Email"] = loginVM.Email;
                
                //回到原本頁面並跳出登入Modal
                return Redirect(Request.UrlReferrer.AbsolutePath);
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