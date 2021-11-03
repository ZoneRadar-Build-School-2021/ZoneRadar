using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;
using System.Web.Security;
using ZoneRadar.Models.ViewModels;
using Newtonsoft.Json;

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

        /// <summary>
        /// 忘記密碼(Get)(Jenny)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        /// <summary>
        /// 忘記密碼(Post)(Jenny)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            //確認是否有此會員
            var hasUserInfo = _service.SearchUser(email, true);
            if (hasUserInfo)
            {
                //寄送重設密碼信
                _service.SentResetPasswordEmail(Server, Request, Url, email);
                return View("HadSentResetPasswordEmail");
            }
            else
            {
                ViewData["Alert"] = true;
                ViewData["Message"] = "找不到此會員，請重新輸入！";
                ViewData["Icon"] = false;
                return View();
            }
        }

        /// <summary>
        /// 重設密碼(Get)(Jenny)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="resetCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResetPassword(string email, string resetCode, string expired)
        {
            var expiredTime = new DateTime();
            DateTime.TryParse(expired, out expiredTime);
            //超過10分鐘無效
            if (DateTime.Now > expiredTime)
            {
                TempData["Alert"] = true;
                TempData["Message"] = "超過10分鐘有效時間，請重新嘗試！";
                TempData["Icon"] = false;
                return RedirectToAction("Index", "Home");
            }
            var user = _service.VerifyResetPasswordLink(email, resetCode);
            if (user != null)
            {
                ViewData["Email"] = user.Email;
                return View();
            }
            else
            {
                TempData["Alert"] = true;
                TempData["Message"] = "找不到此會員，請重新嘗試！";
                TempData["Icon"] = false;
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// 重設密碼(Post)(Jenny)
        /// </summary>
        /// <param name="resetPasswordVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ResetPassword(ResetZONERadarPasswordViewModel resetPasswordVM)
        {
            if (!ModelState.IsValid || resetPasswordVM.NewPassword != resetPasswordVM.NewConfirmPassword)
            {
                //輸入格式不正確或密碼不一致
                ViewData["Alert"] = true;
                ViewData["Message"] = "輸入格式不正確或密碼不一致，請重新輸入！";
                ViewData["Icon"] = false;
                return View();
            }
            //如果已重設密碼、登入了，又按上一頁
            if (User.Identity.IsAuthenticated)
            {
                TempData["Alert"] = true;
                TempData["Message"] = "現為登入狀態，無法以此方式重設密碼！";
                TempData["Icon"] = false;
                return RedirectToAction("Index", "Home");
            }

            //修改會員密碼
            var memberResult = _service.EditPassword(resetPasswordVM);
            if (memberResult.IsSuccessful)
            {
                //讓使用者登入，呈現登入後的畫面
                //建造加密表單驗證票證
                var encryptedTicket = _service.CreateEncryptedTicket(memberResult.User);

                //建造cookie
                _service.CreateCookie(encryptedTicket, Response);

                TempData["Alert"] = true;
                TempData["Message"] = memberResult.ShowMessage;
                TempData["Icon"] = memberResult.IsSuccessful;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //密碼修改失敗，回去首頁，跳出錯誤訊息
                TempData["Alert"] = true;
                TempData["Message"] = memberResult.ShowMessage;
                TempData["Icon"] = memberResult.IsSuccessful;
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// 註冊(Get)(Jenny)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            //若直接輸入路由：導回原本頁面並跳出註冊Modal(非登入狀態時)
            //防止登入狀態下輸入路由
            if (!User.Identity.IsAuthenticated)
            {
                TempData["RegisterModalPopup"] = true;
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 註冊(Post)(Jenny)
        /// </summary>
        /// <param name="registerVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Name, RegisterEmail, RegisterPassword, ConfirmPassword")] RegisterZONERadarViewModel registerVM)
        {
            if (!ModelState.IsValid || registerVM.RegisterPassword != registerVM.ConfirmPassword)
            {
                //輸入格式不正確或密碼不一致
                TempData["Alert"] = true;
                TempData["Message"] = "輸入格式不正確或密碼不一致，請重新註冊！";
                TempData["Icon"] = false;
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            else
            {
                //先將註冊資訊存進資料庫中
                var memberResult = _service.RegisterMember(registerVM);
                //如果註冊資訊成功儲存
                if (memberResult.IsSuccessful)
                {
                    //測試：用Session記錄註冊資訊
                    //Session["ConfirmRegister"] = new List<string>() { registerResult.User.Email, DateTime.Now.AddMinutes(10).ToString() };

                    //接著寄送驗證信
                    _service.SentEmail(Server, Request, Url, memberResult.User.Email);

                    ViewData["UserEmail"] = memberResult.User.Email;
                    return View("HadSentEmail");
                }
                else
                {
                    //註冊失敗
                    TempData["Alert"] = true;
                    TempData["Message"] = memberResult.ShowMessage;
                    TempData["Icon"] = memberResult.IsSuccessful;
                    return Redirect(Request.UrlReferrer.AbsolutePath);
                }
            }
        }

        /// <summary>
        /// 重發驗證信(Get)(Jenny)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResentVerificationEmail()
        {
            return View();
        }

        /// <summary>
        /// 重發驗證信(Post)(Jenny)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public string ResentVerificationEmail(string email)
        {
            //Ajax
            //確認是否有此帳號的註冊紀錄但還未驗證
            var hasRegisterInfo = _service.SearchUser(email, false);
            if (hasRegisterInfo)
            {
                //寄送驗證信
                _service.SentEmail(Server, Request, Url, email);
                var result = new { Icon = "success", Message = "已重發驗證信，請至信箱確認！" };
                var jsonResult = JsonConvert.SerializeObject(result);
                return jsonResult;
            }
            else
            {
                var result = new { Icon = "error", Message = "該帳號不符合未驗證條件，請重新註冊！" };
                var jsonResult = JsonConvert.SerializeObject(result);
                return jsonResult;
            }
        }

        /// <summary>
        /// 驗證電子郵件(Jenny)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        public ActionResult ConfirmEmail(string email, string expired)
        {
            //測試：Session是否接收的到資料？(答：收不到，是null)
            //var test = (List<string>)Session["ConfirmRegister"];
            //var xxx = Request.Cookies["ConfirmRegister"].Value;

            var expiredTime = new DateTime();
            DateTime.TryParse(expired, out expiredTime);
            //超過10分鐘無效
            if (DateTime.Now > expiredTime)
            {
                TempData["Alert"] = true;
                TempData["Message"] = "超過10分鐘有效時間，請重新註冊！";
                TempData["Icon"] = false;
                return RedirectToAction("Index", "Home");
            }
            //確認是否有此註冊資訊
            var memberResult = _service.ConfirmRegister(email);

            //註冊成功
            if (memberResult.IsSuccessful)
            {
                //讓使用者登入，呈現登入後的畫面
                var encryptedTicket = _service.CreateEncryptedTicket(memberResult.User);
                _service.CreateCookie(encryptedTicket, Response);
                //導回原本的畫面
                var originalUrl = _service.GetOriginalUrl(memberResult.User.MemberID.ToString());

                TempData["Alert"] = true;
                TempData["Message"] = memberResult.ShowMessage;
                TempData["Icon"] = memberResult.IsSuccessful;
                return Redirect(originalUrl);
            }
            else
            {
                //註冊失敗，回去首頁，跳出錯誤訊息
                TempData["Alert"] = true;
                TempData["Message"] = memberResult.ShowMessage;
                TempData["Icon"] = memberResult.IsSuccessful;
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// 登入(Get)(Jenny)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            //防止登入狀態下輸入路由
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginModalPopup"] = true;
            }         
            //如果想進入未授權畫面
            if (Request.QueryString["ReturnUrl"] != null)
            {
                //導回原本所在的畫面上，攜帶ReturnUrl參數，並跳出登入Modal
                return Redirect($"{Request.UrlReferrer.AbsolutePath}?ReturnUrl={Request.QueryString["ReturnUrl"]}");
            }

            //若直接輸入路由：導回原本頁面並跳出登入Modal(非登入狀態下)
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 登入(Post)(Jenny)
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "LoginEmail, LoginPassword")] LoginZONERadarViewModel loginVM)
        {
            //若未通過Model驗證(前端已先驗證過)
            if (!ModelState.IsValid)
            {
                //回到原本頁面並跳出錯誤訊息
                TempData["Alert"] = true;
                TempData["Message"] = "輸入格式不正確，請重新登入！";
                TempData["Icon"] = false;
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }

            var memberResult = _service.UserLogin(loginVM);

            //找不到此會員
            if (memberResult.User == null)
            {
                TempData["Alert"] = true;
                TempData["Message"] = memberResult.ShowMessage;
                TempData["Icon"] = memberResult.IsSuccessful;
                //TempData["Email"] = loginVM.Email;
                //TempData["Email"] = ModelState["LoginZONERadarVM.Email"];

                //回到原本頁面並跳出錯誤訊息
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }

            //建造加密表單驗證票證
            var encryptedTicket = _service.CreateEncryptedTicket(memberResult.User);

            //建造cookie
            _service.CreateCookie(encryptedTicket, Response);

            //導向使用者原先欲造訪的路由
            var originalUrl = _service.GetOriginalUrl(memberResult.User.MemberID.ToString());

            TempData["Alert"] = true;
            TempData["Message"] = memberResult.ShowMessage;
            TempData["Icon"] = memberResult.IsSuccessful;
            return Redirect(originalUrl);
        }

        /// <summary>
        /// 登出(Jenny)
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 會員的收藏場地 (Jack)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 會員的資訊 (Jack)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 場地主的資訊 (Jack)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// (路由)場地主 (Jack)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// (路由)會員 (Jack)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// (路由)會員的收藏場地 (Jack)
        /// </summary>
        /// <returns></returns>
        public ActionResult Collection(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                var MemCollectionSpaces = _service.GetMemberCollection(id.Value);
                return View("MyCollection", MemCollectionSpaces);
            }
        }

        /// <summary>
        /// 編輯個人資料頁面(昶安)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProfile()
        {
            var userID = int.Parse(User.Identity.Name); //特定帳號
            var model = _service.GetProfileData(userID);
            return View(model);
        }

        /// <summary>
        /// 編輯個人資料頁面(昶安)
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile([Bind(Include = "MemberID,Photo,Name,Email,Phone,Description,ReceiveEDM")] ProfileViewModel edit)
        {
            if (ModelState.IsValid)
            {
                var model = _service.EditProfileData(edit);
                return View(model);
            }
            return View("EditProfile");
        }

        public void GoogleLoginCallback()
        {

        }
    }
}