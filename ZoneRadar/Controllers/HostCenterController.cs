using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Models;

namespace ZoneRadar.Controllers
{
    public class HostCenterController : Controller
    {
        private readonly SpaceService _spaceService;
        private readonly OrderService _orderService;
       

        // GET: HostCenter
        public HostCenterController()
        {
            _spaceService = new SpaceService();
            _orderService = new OrderService();
        }

        // GET: HostCenter
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  場地主上架場地(Amber) 
        /// </summary>
        public ActionResult AddSpace()
        {
            var model = new SpaceViewModel
            {
                SpaceTypeAraeList = _spaceService.ShowSpaceType().SpaceTypeAraeList,
                cancellationAraesList = _spaceService.ShowCancellations().cancellationAraesList,
                addressAraeList = _spaceService.ShowAmenityByIdOne().addressAraeList,

                amenityAraeOneList = _spaceService.ShowAmenityByIdOne().amenityAraeOneList,
                amenityAraeTwoList = _spaceService.ShowAmenityByIdTwo().amenityAraeTwoList,
                amenityAraeThreeList = _spaceService.ShowAmenityByIdThree().amenityAraeThreeList,

                CleanFisrtPartList = _spaceService.ShowCleaningCategoryByIdOne().CleanFisrtPartList,
                CleanSecPartList = _spaceService.ShowCleaningCategoryByIdTwo().CleanSecPartList,
                CleanThirdPartList = _spaceService.ShowCleaningCategoryByIdThree().CleanThirdPartList,
                CleanFourdPartList = _spaceService.ShowCleaningCategoryByIdFour().CleanFourdPartList,
                //SomeOnesSpaceNameList = _spaceService.ShowOwnerName().SomeOnesSpaceNameList,

                Operating = _spaceService.Operating(),
            };

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        //public ActionResult AddSpace(AddSpaceViewModel space, AddOperatingViewModel addOperating)

        public ActionResult AddSpace(AddSpaceViewModel space)
        {
            var result = _spaceService.CreateSpace(space);
            ViewData["Message"] = "成功新增場地";
            return View(result);
        }
        /// <summary>
        /// 場地主編輯場地 (Amber)
        /// </summary>
        public ActionResult EditSpace(int spaceId)
        {
            var model = _spaceService.ReadAnySpace(spaceId);
            
            return View(model);
        }
        /// <summary>
        /// 編輯場地(Steve) 
        /// </summary>
        /// <returns></returns>
        public ActionResult SpaceManage()
        {
            return View();
        }

        /// <summary>
        /// 場地主訂單 - 處理中(Jack)
        /// </summary>
        /// <returns></returns>
        public ActionResult Processing()
        {
            var userid = int.Parse(User.Identity.Name);
            var result = _orderService.GetHostCenter(userid);
            return View(result);
        }

        /// <summary>
        /// 場地主訂單 - 歷史訂單(Steve) 
        /// </summary>
        /// <returns></returns>
        public ActionResult History()
        {
            return View();
        }
    }
}