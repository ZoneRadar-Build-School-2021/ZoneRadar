using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Controllers
{
    public class MemberCenterController : Controller
    {
        // GET: MemberCenter
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SellerCenter()
        {
            return View();
        }
        public ActionResult AddSpace()
        {
            return View();
        }
        public ActionResult ProfileInfo()
        {
            return View();
        }
        public ActionResult Pending()
        {
            return View();
        }
        public ActionResult Processing()
        {
            return View();
        }
        public ActionResult Completed()
        {
            return View();
        }

    }
}