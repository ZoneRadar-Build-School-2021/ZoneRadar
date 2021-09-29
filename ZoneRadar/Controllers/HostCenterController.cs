using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;
using ZoneRadar.Models;
using ZoneRadar.Repositories;
using ZoneRadar.Models.ViewModels;

namespace ZoneRadar.Controllers
{
    public class HostCenterController : Controller
    {
        private readonly SpaceService _spaceService;

        // GET: HostCenter
       public HostCenterController() 
        {
            _spaceService = new SpaceService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddSpace()
        {

            var model = new SpaceViewModel
            {
                Spacetype = _spaceService.ShowSpaceType(),
                Parking=_spaceService.ShowParking(),
                amenityAraeList=_spaceService.ShowSpaceSelect().amenityAraeList,
                cancellationAraesList=_spaceService.ShowSpaceSelect().cancellationAraesList,
                CleanFisrtPart=_spaceService.ShowCleanFisrt(),
                CleanSecPart=_spaceService.ShowCleanSec(),
                CleanThirdPart=_spaceService.ShowCleanThird(),
                CleanFourthPart=_spaceService.ShowCleanFourth(),
                Operating= _spaceService.Operating()
            };
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