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
                SpaceTypeAraeList=_spaceService.ShowSpaceType().SpaceTypeAraeList,
                cancellationAraesList=_spaceService.ShowCancellations().cancellationAraesList,
                addressAraeList=_spaceService.ShowAmenityByIdOne().addressAraeList,
                
                amenityAraeOneList=_spaceService.ShowAmenityByIdOne().amenityAraeOneList,
                amenityAraeTwoList=_spaceService.ShowAmenityByIdTwo().amenityAraeTwoList,
                amenityAraeThreeList=_spaceService.ShowAmenityByIdThree().amenityAraeThreeList,
                
                CleanFisrtPartList=_spaceService.ShowCleaningCategoryByIdOne().CleanFisrtPartList,
                CleanSecPartList=_spaceService.ShowCleaningCategoryByIdTwo().CleanSecPartList,
                CleanThirdPartList=_spaceService.ShowCleaningCategoryByIdThree().CleanThirdPartList,
                CleanFourdPartList=_spaceService.ShowCleaningCategoryByIdFour().CleanFourdPartList,

                Operating= _spaceService.Operating(),
               
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