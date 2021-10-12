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

        /// <summary>
        /// 編輯場地(Steve)
        /// </summary>
        /// <returns></returns>
        public ActionResult EditSpace()
        {
            return View();
        }
    }
}