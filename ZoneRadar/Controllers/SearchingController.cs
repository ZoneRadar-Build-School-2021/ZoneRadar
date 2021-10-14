using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models.ViewModels;
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
        [HttpGet]
        public ActionResult SearchingPage()
        {
            return View();
        }

        /// <summary>
        /// 搜尋頁(Post)(Jenny)
        /// </summary>
        /// <param name="queryVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchingPage(QueryViewModel queryVM)
        {
            return View(queryVM);
        }

        /// <summary>
        /// GET: Type/聚餐(Jenny)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult SearchByType(string type)
        {
            var queryVM = new QueryViewModel { Type = type };
            return View("SearchingPage", queryVM);
        }

        /// <summary>
        /// GET: City/台北市(Jenny)
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public ActionResult SearchByCity(string city)
        {
            var queryVM = new QueryViewModel { City = city };
            return View("SearchingPage", queryVM);
        }

    }
}