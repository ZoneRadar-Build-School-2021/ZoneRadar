using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class HomeController : Controller
    {
        private readonly IndexService _ser;
        public HomeController()
        {
            _ser = new IndexService();
        }

        public ActionResult Index()
        {
            return View(_ser.GetSpaceVMData());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Policy()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult NotFound404()
        {
            return View();
        }
    }
}