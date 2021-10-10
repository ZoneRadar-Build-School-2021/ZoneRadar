using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class SearchingController : Controller
    {
        private readonly SpaceService _spaceService;
        public SearchingController()
        {
            _spaceService = new SpaceService();
        }

        /// <summary>
        /// 搜尋頁(Steve)
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchingPage()
        {
            return View();
        }

    }
}