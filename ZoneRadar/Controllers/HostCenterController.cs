using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Controllers
{
    public class HostCenterController : Controller
    {
        // GET: HostCenter
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSpace()
        {
            return View();
        }
        public ActionResult EditSpace()
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