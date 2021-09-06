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
        //賣家中心的訂單
        public ActionResult SellerOrder()
        {
            return View();
        }
        public ActionResult SellerOrderCompleted()
        {
            return View();
        }
        //賣家中心的場地管理 新增場地
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