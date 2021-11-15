using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Data;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    [Authorize]
    public class UserCenterController : Controller
    {
        private readonly OrderService _OrderService;
        private readonly PreOrderService _PreOrderService;
        private readonly ReviewService _ReviewService;
        private readonly ZONERadarContext _db;
        public UserCenterController()
        {
            _OrderService = new OrderService();
            _PreOrderService = new PreOrderService();
            _ReviewService = new ReviewService();

            _db = new ZONERadarContext();
        }
        // GET: UserCenter
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 訂單已付款頁(Nick)
        /// </summary>
        /// <returns></returns>
        public ActionResult Pending()
        {
            var userId = int.Parse(User.Identity.Name);
            var model = _OrderService.GetUsercenterPendingVM(userId);

            return View(model);
        }
        /// <summary>
        /// 訂單使用中頁(Nick)
        /// </summary>
        /// <returns></returns>
        public ActionResult Processing()
        {
            var userid = int.Parse(User.Identity.Name);
            var model = _OrderService.GetUsercenterProcessingVM(userid);

            return View(model);
        }
        /// <summary>
        /// 訂單已完成頁(Nick)
        /// </summary>
        /// <returns></returns>
        public ActionResult Completed()
        {
            var userid = int.Parse(User.Identity.Name);
            var model = _OrderService.GetUsercenterCompletedVM(userid);

            return View(model);
        }
        /// <summary>
        /// 預購單頁(Nick)
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopCar()
        {
            var userid = int.Parse(User.Identity.Name);
            var model = _PreOrderService.GetShopCarVM(userid);

            return View(model);
        }
        /// <summary>
        /// (Get)編輯預購單細項頁(Nick)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditShopCarDetail()
        {
            var userid = int.Parse(User.Identity.Name);

            if (userid == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _PreOrderService.GetShopCarVM(userid);

            return View("ShopCar", model);
        }
        /// <summary>
        /// (Post)編輯預購單細項頁(Nick)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditShopCarDetail(RentDetailViewModel model)
        {
            var userid = int.Parse(User.Identity.Name);
            if (ModelState.IsValid)
            {
                var result = _PreOrderService.EditShopCarDetail(model);
                return RedirectToAction("ShopCar", result);
            }
            var resultmodel = _PreOrderService.GetShopCarVM(userid);
            return View("ShopCar", resultmodel);
        }
        /// <summary>
        /// 刪除預購單細項頁(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteShopCarDetail(int id)
        {
            var result = _PreOrderService.DeleteShopCarDetail(id);
            return RedirectToAction("ShopCar", result);
        }
        /// <summary>
        /// 刪除預購單頁(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteShopCarOrder(int id)
        {
            var result = _PreOrderService.DeleteShopCarOrder(id);
            return RedirectToAction("ShopCar", result);
        }
        /// <summary>
        /// (Get)刪除已付款訂單頁(Nick)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeletePendingOrder()
        {
            var userid = int.Parse(User.Identity.Name);

            if (userid == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _OrderService.GetUsercenterPendingVM(userid);

            return View("Pending", model);
        }
        /// <summary>
        /// (Post)刪除已付款訂單頁(Nick)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeletePendingOrder(UsercenterPendingViewModel model)
        {
            var userid = int.Parse(User.Identity.Name);
            if (ModelState.IsValid)
            {
                _OrderService.DeletePendingOrder(model);
                return RedirectToAction("Pending");
            }
            var resultmodel = _OrderService.GetUsercenterPendingVM(userid);
            return View("Pending", resultmodel);
        }
        /// <summary>
        /// (Get)新增已完成訂單評價頁(Nick)
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
            var model = _OrderService.GetUsercenterCompletedVM(userid);

            return View("Completed", model);
        }
        /// <summary>
        /// (Post)新增已完成訂單評價頁(Nick)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreatCompletedReview(UsercenterCompletedViewModel model)
        {
            var result = _ReviewService.CreatCompletedReview(model);
            return RedirectToAction("Completed", result);
        }
    }
}