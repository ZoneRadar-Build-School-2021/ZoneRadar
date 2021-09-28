using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;
using ZoneRadar.Models;

namespace ZoneRadar.Controllers
{
    
    public class HostCenterController : Controller
    {
        private readonly AmenityService _amenityService;
        // GET: HostCenter
       public HostCenterController() 
        {
            _amenityService = new AmenityService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSpace()
        {
            var model = _amenityService.ShowSelectofCheckBox();
            return View(model);
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