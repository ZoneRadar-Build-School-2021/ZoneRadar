using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;
using System.Web.Security;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class MemberCenterController : Controller
    {
        private readonly MemberService _memberservice;
        //private readonly HostInfoService _hostinfoservice;
       // private readonly MyCollectionService _myCollectionservice;
        public MemberCenterController()
        {
            _memberservice = new MemberService();
        }
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
        public ActionResult MyCollection(int? memberId)
        {
            if (!memberId.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                var MemCollectionSpaces = _memberservice.GetMemberCollection(memberId.Value);
                return View(MemCollectionSpaces);
            }
        }
        public ActionResult UserInfo(int? memberId)
        {
            if (!memberId.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                var HostReview = _memberservice.GetHostReview(memberId.Value);
                return View(HostReview);
            }
        }
        public ActionResult HostInfo(int? memberId)
        {
            if (!memberId.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else 
            { 
               var MemSpace = _memberservice.GetMemberSpace(memberId.Value);
               return View(MemSpace);
            }
        }
        //場地主ID
        public ActionResult HostId(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                var MemSpace = _memberservice.GetMemberSpace(id.Value);
                return View("HostInfo",MemSpace);
            }
        }
        //會員
        public ActionResult MemberInfo(int? id)
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
        public ActionResult Register([Bind(Include = "Name, Email, Password, ConfirmPassword")] RegisterZONERadarViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            else
            {               
                var registerResult = _service.RegisterMember(registerVM);
                //註冊成功
                if (registerResult.IsSuccessful)
                {
                    var encryptedTicket = _service.CreateEncryptedTicket(registerResult.User);
                    _service.CreateCookie(encryptedTicket, Response);
                    return Redirect(_service.GetReturnUrl(registerResult.User.MemberID.ToString()));
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
            //如果想進入未授權畫面
            if (Request.QueryString["ReturnUrl"] != null)
            {
                //導回原本所在的畫面上，攜帶ReturnUrl，並跳出登入Modal
                return Redirect($"{Request.UrlReferrer.AbsolutePath}?ReturnUrl={Request.QueryString["ReturnUrl"]}");
            }
            //(目前專案狀況都不會執行到這一行)
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email, Password")] LoginZONERadarViewModel loginVM)
        {
            //若未通過Model驗證(前端已先驗證過)
            if (!ModelState.IsValid)
            {
                //回到原本頁面並跳出登入Modal
                TempData["LoginModalPopup"] = true;
                Response.Redirect(Request.UrlReferrer.AbsolutePath);
                //return View();
            }

            var user = _service.UserLogin(loginVM);
                       
            //找不到則彈回Login頁
            if (user == null)
            {
                TempData["LoginModalPopup"] = true;
                TempData["Email"] = loginVM.Email;
                //TempData["Email"] = ModelState["LoginZONERadarVM.Email"];
                
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