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
        //private readonly MyCollectionService _myCollectionservice;
        private readonly HostInfoService _hostinfoservice;
        private readonly MyCollectionService _mycollectionservice;
        public MemberCenterController()
        {
            _hostinfoservice = new HostInfoService();
            _mycollectionservice = new MyCollectionService();
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
                return RedirectToRoute(new { controller ="Home" , action ="Index" });
            }
            
            var MemCollection = _mycollectionservice.GetMemCollection(memberId.Value);
            
            return View(MemCollection);
        }
        public ActionResult UserInfo()
        {
            return View();
        }
        public ActionResult HostInfo(int? memberId)
        {
            if (!memberId.HasValue)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var MemSpaces = _hostinfoservice.GetMemberSpace(memberId.Value);
            return View(MemSpaces);
        }
    }
}