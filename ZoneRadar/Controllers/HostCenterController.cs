using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Services;
using ZoneRadar.Models.ViewModels;
using System.Net;

namespace ZoneRadar.Controllers
{
    public class HostCenterController : Controller
    {
        private readonly SpaceService _spaceService;
        private readonly OrderService _orderService;
        private readonly ReviewService _reviewService;

        // GET: HostCenter
        public HostCenterController()
        {
            _spaceService = new SpaceService();
            _orderService = new OrderService();
            _reviewService = new ReviewService();
        }

        // GET: HostCenter
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  場地主上架場地(Amber) 
        /// </summary>
        [Authorize]
        public ActionResult AddSpace(SpaceViewModel id)
        {
            var userId = 0;
            var isAuthenticated = int.TryParse(User.Identity.Name, out userId);
            if (isAuthenticated)
            {
                var model = new SpaceViewModel( )
                {
                    SpaceTypeAraeList = _spaceService.ShowSpaceType().SpaceTypeAraeList,
                    cancellationAraesList = _spaceService.ShowCancellations().cancellationAraesList,

                    amenityAraeOneList = _spaceService.ShowAmenityByIdOne().amenityAraeOneList,
                    amenityAraeTwoList = _spaceService.ShowAmenityByIdTwo().amenityAraeTwoList,
                    amenityAraeThreeList = _spaceService.ShowAmenityByIdThree().amenityAraeThreeList,

                    CleanFisrtPartList = _spaceService.ShowCleaningCategoryByIdOne().CleanFisrtPartList,
                    CleanSecPartList = _spaceService.ShowCleaningCategoryByIdTwo().CleanSecPartList,
                    CleanThirdPartList = _spaceService.ShowCleaningCategoryByIdThree().CleanThirdPartList,
                    CleanFourdPartList = _spaceService.ShowCleaningCategoryByIdFour().CleanFourdPartList,
                    //SomeOnesSpaceNameList = _spaceService.ShowOwnerName().SomeOnesSpaceNameList,
                    SpaceId=_spaceService.GetSpaceId().SpaceId,
                    Operating = _spaceService.Operating(),
                };
                return View(model);
            }
           
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]

        public ActionResult AddSpace(AddSpaceViewModel space)
        {
            var userid = int.Parse(User.Identity.Name);
            space.MemberID = userid;
            var result = _spaceService.CreateSpace(space);
            return RedirectToAction("SpaceManage", "HostCenter");
        }
        /// <summary>
        /// 場地主編輯場地 (Amber)
        /// Get
        /// </summary>
        [HttpGet]
        public ActionResult EditSpace(int spaceId)
        {
            var model = _spaceService.ReadAnySpace(spaceId);
            
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditSpace( AddSpaceViewModel editspace)
        {
            var userid = int.Parse(User.Identity.Name);
            editspace.MemberID = userid;

            var result = _spaceService.EditSpace(editspace);
            return RedirectToAction("SpaceManage", result);
        }

        /// <summary>
        /// 管理我的場地(Jenny)
        /// </summary>
        /// <returns></returns>
        public ActionResult SpaceManage()
        {
            var parseId =  int.TryParse(User.Identity.Name, out int userId);
            if (User.Identity.IsAuthenticated)
            {
                var spaceManageList = _spaceService.GetHostSpace(userId);
                ViewData["Alert"] = TempData["Alert"];
                ViewData["Message"] = TempData["Message"];
                ViewData["Icon"] = TempData["Icon"];
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
        /// 儲存場地預定下架日期(Jenny)
        /// </summary>
        /// <param name="spaceId"></param>
        /// <param name="discontinuedDate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SpaceDiscontinue(int spaceId, DateTime? discontinuedDate)
        {
            var parseId = int.TryParse(User.Identity.Name, out int userId);
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (discontinuedDate.HasValue)
            {
                var spaceStatusInfo = new SpaceStatusInformation()
                {
                    UserId = userId,
                    SpaceId = spaceId,
                    SpaceStatusId = (int)SpaceStatusEnum.Discontinued,
                    DiscontinuedDate = discontinuedDate.Value
                };
                var sweetAlert = _spaceService.SetSpaceStatus(spaceStatusInfo);
                TempData["Alert"] = sweetAlert.Alert;
                TempData["Message"] = sweetAlert.Message;
                TempData["Icon"] = sweetAlert.Icon;
                return RedirectToAction("SpaceManage");
            }
            else
            {
                //防呆未完成
                return RedirectToAction("SpaceManage");
            }
        }

        /// <summary>
        /// 取消下架(Jenny)
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CancelDiscontinue(int spaceId)
        {
            var parseId = int.TryParse(User.Identity.Name, out int userId);
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //取消下架：SpaceStatusEnum設為Discontinued，但時間是null
                var spaceStatusInfo = new SpaceStatusInformation()
                {
                    UserId = userId,
                    SpaceId = spaceId,
                    SpaceStatusId = (int)SpaceStatusEnum.Discontinued
                };
                var sweetAlert = _spaceService.SetSpaceStatus(spaceStatusInfo);
                TempData["Alert"] = sweetAlert.Alert;
                TempData["Message"] = sweetAlert.Message;
                TempData["Icon"] = sweetAlert.Icon;
                return RedirectToAction("SpaceManage");
            }
        }

        /// <summary>
        /// 刪除場地(Jenny)
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteSpace(int spaceId)
        {
            var parseId = int.TryParse(User.Identity.Name, out int userId);
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var spaceStatusInfo = new SpaceStatusInformation()
                {
                    UserId = userId,
                    SpaceId = spaceId,
                    SpaceStatusId = (int)SpaceStatusEnum.Delete
                };
                var sweetAlert = _spaceService.SetSpaceStatus(spaceStatusInfo);
                TempData["Alert"] = sweetAlert.Alert;
                TempData["Message"] = sweetAlert.Message;
                TempData["Icon"] = sweetAlert.Icon;
                return RedirectToAction("SpaceManage");
            }
        }

        /// <summary>
        /// 重新上架(Jenny)
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RepublishSpace(int spaceId)
        {
            var parseId = int.TryParse(User.Identity.Name, out int userId);
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var spaceStatusInfo = new SpaceStatusInformation()
                {
                    UserId = userId,
                    SpaceId = spaceId,
                    SpaceStatusId = (int)SpaceStatusEnum.OnTheShelf
                };
                var sweetAlert = _spaceService.SetSpaceStatus(spaceStatusInfo);
                TempData["Alert"] = sweetAlert.Alert;
                TempData["Message"] = sweetAlert.Message;
                TempData["Icon"] = sweetAlert.Icon;
                return RedirectToAction("SpaceManage");
            }
        }
        /// <summary>
        /// (Get)場地主新增歷史訂單評價頁(Nick)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreatCompletedReview()
        {
            var userid = int.Parse(User.Identity.Name);

            if (userid == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _orderService.GetHostCenterHistoryVM(userid);

            return View("History", model);
        }
        /// <summary>
        /// (Post)場地主新增歷史訂單評價頁(Nick)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreatCompletedReview(HostCenterHistoryViewModel model)
        {
            var userid = int.Parse(User.Identity.Name);

            var result = _reviewService.CreateHistoryReview(model);
            return RedirectToAction("History", result);
        }
    }
}