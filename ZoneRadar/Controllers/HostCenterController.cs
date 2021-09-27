using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Service;

namespace ZoneRadar.Controllers
{
    
    public class HostCenterController : Controller
    {
        private readonly addressAreaService _areaService;
        // GET: HostCenter
       public HostCenterController() 
        {
            _areaService = new addressAreaService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSpace()
        {
            var model = _areaService.readFormAreaSelect();
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