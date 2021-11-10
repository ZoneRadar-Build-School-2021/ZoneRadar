using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    public class BookingController : Controller
    {
        private readonly SpaceService _spaceService;
        private readonly ReviewService _reviewService;
        public BookingController()
        {
            _spaceService = new SpaceService();
            _reviewService = new ReviewService();
        }

        /// <summary>
        /// 預約頁面(Steve)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BookingPage(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("BadRequest", "Home");
            }

            var targetSpace = _spaceService.GetSpaceByID(id);
            if (targetSpace == null)
            {
                return RedirectToAction("NotFound404", "Home");
            }

            var model = new BookingPageViewModel
            {
                SpaceBreifInfo = _spaceService.GetTargetSpaceBriefInfo(targetSpace),
                SpaceDetailInfo = _spaceService.GetTargetSpaceDetail(targetSpace),
                Reviews = _reviewService.GetTargetSpaceReviews(targetSpace)
            };

            return View(model);
        }
    }
}
