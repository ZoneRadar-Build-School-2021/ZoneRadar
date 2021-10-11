using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;
using System.Web.Security;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;
using ZoneRadar.ViewModels;
using ZoneRadar.Models;
using ZoneRadar.Data;
using System.Data.Entity;

namespace ZoneRadar.Controllers
{
    public class MemberCenterController : Controller
    {
        private readonly MemberService _service;
        private readonly ZONERadarContext _db;
        public MemberCenterController()
        {
            _service = new MemberService();
            _db = new ZONERadarContext();
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
            var userID = int.Parse(User.Identity.Name); //登入後的帳號(綁定一人)
            var model = _service.GetProfileData(userID);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include ="MemberID,Name,Email,Phone,Description")] ProfileViewModel edit)
        {
            //判斷是否通過驗證
            if (ModelState.IsValid)
            {
                //取出 ->編輯資料
                var userID = int.Parse(User.Identity.Name);
                Member p = _db.Member.FirstOrDefault(x => x.MemberID == userID);
                p.Name = edit.Name;
                p.Phone = edit.Phone;
                p.Description = edit.Description;

                //將p的狀態設為modified
                _db.Entry(p).State = EntityState.Modified;
                //儲存資料,並向SQL Server發出update指令
                _db.SaveChanges();

                return RedirectToAction("EditProfile");
            }
            return View(edit);
        }

        public ActionResult MyCollection(int? memberId)
        {
            if (!memberId.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                var MemCollectionSpaces = _service.GetMemberCollection(memberId.Value);
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
                var HostReview = _service.GetHostReview(memberId.Value);
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
               var MemSpace = _service.GetMemberSpace(memberId.Value);
               return View(MemSpace);
            }
        }
        //場地主ID
        public ActionResult Host(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                var MemSpace = _service.GetMemberSpace(id.Value);
                return View("HostInfo", MemSpace);
            }
        }
        //會員
        public ActionResult Member(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                var MemSpace = _service.GetHostReview(id.Value);
                return View("UserInfo", MemSpace);
            }
        }
        //收藏
        public ActionResult Collection(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                var MemCollectionSpaces = _service.GetMemberCollection(id.Value);
                return View("MyCollection",MemCollectionSpaces);
            }
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
        public ActionResult VerifyMail()
        {
            return View();
        }
    }
}