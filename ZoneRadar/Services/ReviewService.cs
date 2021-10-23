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
        /// 新增完成訂單的評價(Nick)
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
                ReviewDate = DateTime.Now,
                Recommend = model.Recommend
            };

            _repository.Create<Review>(review);
            _repository.SaveChanges();


            return review;
        }   
        
        public Review CreateHistoryReview(HostCenterHistoryViewModel model)
        {
            var review = new Review
            {
                OrderID = model.OrderId,
                ToHost = false,
                Score = (int)model.Score,
                ReviewContent = model.ReviewContent,
                ReviewDate = DateTime.Now,
                Recommend = model.Recommend
            };

            _repository.Create<Review>(review);
            _repository.SaveChanges();

            return review;
        }
        /// <summary>
        /// 將場地狀態資訊存進資料庫(下架、刪除、重新上架)(Jenny)
        /// </summary>
        public SweetAlert SetSpaceStatus(SpaceStatusInformation spaceStatusInfo)
        {
            var sweetAlert = new SweetAlert { Alert = false };
            try
            {
                var space = _repository.GetAll<Space>().FirstOrDefault(x => x.SpaceID == spaceStatusInfo.SpaceId && x.MemberID == spaceStatusInfo.UserId);
                if (space != null)
                {
                    //儲存預定下架日期
                    if (spaceStatusInfo.SpaceStatusId == (int)SpaceStatusEnum.Discontinued && spaceStatusInfo.DiscontinuedDate != null)
                    {
                        space.DiscontinuedDate = spaceStatusInfo.DiscontinuedDate.Value;
                        //如果預定下架日期是當天，直接將場地狀態改成下架
                        if (spaceStatusInfo.DiscontinuedDate.Value.Date == DateTime.Today)
                        {
                            space.SpaceStatusID = (int)SpaceStatusEnum.Discontinued;
                        }
                    }
                    //取消下架
                    if (spaceStatusInfo.SpaceStatusId == (int)SpaceStatusEnum.Discontinued && spaceStatusInfo.DiscontinuedDate == null)
                    {
                        space.DiscontinuedDate = null;
                    }
                    //重新上架
                    if (spaceStatusInfo.SpaceStatusId == (int)SpaceStatusEnum.OnTheShelf)
                    {
                        space.SpaceStatusID = (int)SpaceStatusEnum.OnTheShelf;
                        space.DiscontinuedDate = null;
                    }
                    //刪除場地
                    if (spaceStatusInfo.SpaceStatusId == (int)SpaceStatusEnum.Delete)
                    {
                        space.SpaceStatusID = (int)SpaceStatusEnum.Delete;
                    }

                    _repository.Update(space);
                    _repository.SaveChanges();
                    return sweetAlert;
                }
                else
                {
                    sweetAlert.Alert = true;
                    sweetAlert.Message = "您並非該場地主";
                    sweetAlert.Icon = false;
                    return sweetAlert;
                }
            }
            catch(Exception ex)
            {
                sweetAlert.Alert = true;
                sweetAlert.Message = "發生錯誤，請稍後再試！";
                sweetAlert.Icon = false;
                return sweetAlert;
            }
        }
    }
}