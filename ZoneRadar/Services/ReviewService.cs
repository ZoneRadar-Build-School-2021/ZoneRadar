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
        /// 找出分數較高的場地評論(Jenny)
        /// </summary>
        /// <returns></returns>
        public List<ToSpaceReviewViewModel> GetSpaceReviews()
        {
            var reviews = _repository.GetAll<Review>().Where(x => x.ToHost).OrderByDescending(x => x.Score).Take(8).ToList();
            var orders = reviews.Select(x => x.Order);
            var members = orders.Select(x => x.Member).Distinct();

            var spaceReviews = new List<ToSpaceReviewViewModel>();

            foreach (var item in reviews)
            {
                spaceReviews.Add(
                    new ToSpaceReviewViewModel
                    {
                        SpaceId = orders.First(x => x.OrderID == item.OrderID).SpaceID,
                        MemberName = members.First(x => x.MemberID == orders.First(y => y.OrderID == item.OrderID).MemberID).Name,
                        ReviewContent = item.ReviewContent,
                        Score = item.Score
                    });
            }

            return spaceReviews;
        }

        /// <summary>
        /// 取得特定場地的評價(Steve)
        /// </summary>
        /// <param name="targetSpace"></param>
        /// <returns></returns>
        public List<SpaceReviewViewModel> GetTargetSpaceReviews(Space targetSpace)
        {
            var reviewList = _repository.GetAll<Review>().Where(x => x.ToHost == true && x.Order.SpaceID == targetSpace.SpaceID).Select(x => x).ToList();

            var spaceReviewList = new List<SpaceReviewViewModel>();

            foreach (var review in reviewList)
            {
                spaceReviewList.Add(new SpaceReviewViewModel
                {
                    Score = review.Score,
                    ReviewContent = review.ReviewContent,
                    ReviewDate = review.ReviewDate,
                    IsRecommend = review.Recommend,
                    ReviewedMemberName = review.Order.Member.Name,
                    ReviewedMemberPhoto = review.Order.Member.Photo,
                    UserID = review.Order.MemberID,
                });
            }

            return spaceReviewList;
        }

        /// <summary>
        /// 新增完成訂單(活動主)的評價(Nick)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Review CreatCompletedReview(UsercenterCompletedViewModel model)
        {
            var review = new Review
            {
                OrderID = model.OrderId,
                ToHost = true,
                Score = (int)model.Score,
                ReviewContent = model.ReviewContent,
                ReviewDate = DateTime.UtcNow.AddHours(8),
                Recommend = model.Recommend
            };

            _repository.Create<Review>(review);
            _repository.SaveChanges();


            return review;
        }
        /// <summary>
        /// 新增完成訂單(場地主)的評價(Nick)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Review CreateHistoryReview(HostCenterHistoryViewModel model)
        {
            var review = new Review
            {
                OrderID = model.OrderId,
                ToHost = false,
                Score = (int)model.Score,
                ReviewContent = model.ReviewContent,
                ReviewDate = DateTime.UtcNow.AddHours(8),
                Recommend = model.Recommend
            };

            _repository.Create<Review>(review);
            _repository.SaveChanges();

            return review;
        }
    }
}