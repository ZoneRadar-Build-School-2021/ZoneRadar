using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Controllers
{
    public class UserCenterController : Controller
    {
        // GET: UserCenter
        public ActionResult Index()
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