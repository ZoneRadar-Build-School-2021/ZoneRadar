using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class ReviewService
    {
        private readonly ZONERadarRepository _repository;
        public ReviewService()
        {
            _repository = new ZONERadarRepository();
        }
        /// <summary>
        /// 找出分數較高的場地評論
        /// </summary>
        /// <returns></returns>
        public List<ToSpaceReviewViewModel> GetSpaceReviews()
        {
            var members = _repository.GetAll<Member>().ToList();
            var reviews = _repository.GetAll<Review>().Where(x => x.ToHost).ToList();
            var orders = _repository.GetAll<Order>().ToList();

            var spaceReviews = new List<ToSpaceReviewViewModel>();

            foreach (var item in reviews)
            {
                spaceReviews.Add(
                    new ToSpaceReviewViewModel
                    {
                        SpaceId = item.Order.SpaceID,
                        MemberName = members.First(x => x.MemberID == item.Order.MemberID).Name,
                        ReviewContent = item.ReviewContent,
                        Score = item.Score
                    });
            }

            var topSpaceReviews = spaceReviews.OrderByDescending(x => x.Score).Take(8).ToList();

            return topSpaceReviews;
        }
    }
}