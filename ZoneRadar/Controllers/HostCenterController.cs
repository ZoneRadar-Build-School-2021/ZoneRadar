using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;
using ZoneRadar.Models.ViewModels;

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
        [ValidateAntiForgeryToken]
        public ActionResult AddSpace(AddSpaceViewModel addspaceVM)
        {


            var model = new SpaceViewModel
            {

            };
            return View(model);
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
        /// 管理我的場地(Jenny)
        /// </summary>
        /// <returns></returns>
        public ActionResult SpaceManage()
        {
            int userId;
            var isAuthenticated = int.TryParse(User.Identity.Name, out userId);
            if (isAuthenticated)
            {
                var spaceManageList = _spaceService.GetHostSpace(userId);
                return View(spaceManageList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
        /// 場地主訂單 - 歷史訂單(Nick) 
        /// </summary>
        /// <returns></returns>
        public ActionResult History()
        {
            var userid = int.Parse(User.Identity.Name);
            var model = _orderService.GetHostCenterHistoryVM(userid);
            return View(model);
        }

        /// <summary>
        /// 下架場地(Jenny)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SpaceDiscontinue(int spaceId, DateTime discontinuedDate)
        {
            int userId;
            var isAuthenticated = int.TryParse(User.Identity.Name, out userId);
            if (isAuthenticated)
            {
                var spaceManageList = _spaceService.GetHostSpace(userId);
                return View(spaceManageList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}