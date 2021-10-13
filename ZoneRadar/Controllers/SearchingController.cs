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


        // GET: Searching
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchingPage()
        {
            //var model = new FilterViewModel
            //{
            //    CityList = _spaceService.GetCityOptions(),
            //    DistrictList = _spaceService
            //    SpaceTypeList = _spaceService.GetTypeOptions(),

            //}



            return View();
        }

        [HttpPost]
        public ActionResult SearchingPage(QueryViewModel queryVM)
        {
            return View(queryVM);
        }

        public ActionResult NoResult()
        {
            return View();
        }
    }
}