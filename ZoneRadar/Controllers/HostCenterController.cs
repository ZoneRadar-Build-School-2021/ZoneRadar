using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;
using ZoneRadar.ViewModels;

namespace ZoneRadar.Controllers
{
    
    public class HostCenterController : Controller
    {
        // GET: HostCenter
        private readonly SpaceService _ser;
       public HostCenterController() 
        {
            _ser = new SpaceService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSpace()
        {
            List<SpaceVM> spaceVMs= _ser.GetSpaceVM();
            
            return View(spaceVMs);
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