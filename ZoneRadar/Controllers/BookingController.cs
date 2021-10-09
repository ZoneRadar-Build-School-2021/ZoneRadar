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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var targetSpace = _spaceService.GetSpaceByID(id);
            if (targetSpace == null)
            {
                return new HttpNotFoundResult();
            }

            var model = new BookingPageViewModel
            {
                SpaceBreifInfo = _spaceService.GetTargetSpaceBriefInfo(targetSpace),
                SpaceDetailInfo = _spaceService.GetTargetSpaceDetail(targetSpace),
                Reviews = _reviewService.GetTargetSpaceReviews(targetSpace)
            };

            return View(model);
        }

        //[HttpGet]
        //public ActionResult AddToCollection()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddToCollection(BookingPageViewModel bookingPageVM)
        //{
        //    var memberID = User.Identity.Name;
        //    var collectionVM = new CollectionViewModel
        //    {
        //        SpaceID = bookingPageVM.SpaceBreifInfo.SpaceID,
        //        SpaceName = bookingPageVM.SpaceBreifInfo.SpaceName,
        //        SpaceImageURL = bookingPageVM.SpaceBreifInfo.SpaceImageURLList[0],
        //        PricePerHour = bookingPageVM.SpaceBreifInfo.PricePerHour,
        //        Country = bookingPageVM.SpaceBreifInfo.Country,
        //        City = bookingPageVM.SpaceBreifInfo.City,
        //        District = bookingPageVM.SpaceBreifInfo.District,
        //        Address = bookingPageVM.SpaceBreifInfo.Address,
        //        Scores = bookingPageVM.Reviews.Select(x => x.Score).ToList()
        //    };

        //    _spaceService.CreateCollectionInDB(bookingPageVM, memberID);
        //    return RedirectToAction("BookingPage", bookingPageVM.SpaceBreifInfo.SpaceID);
        //}

    }
}
