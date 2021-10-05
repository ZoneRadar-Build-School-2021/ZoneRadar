using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            _service = new MemberService();
            _memberservice = new MemberService();
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
    }
}