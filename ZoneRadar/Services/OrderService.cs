﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class OrderService
    {
        private readonly ZONERadarRepository _repository;
        public OrderService()
        {
            _repository = new ZONERadarRepository();
        }

        /// <summary>
        /// 找出使用者(ID)的已付款資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UsercenterPendingViewModel> GetUsercenterPendingVM(int userid)
        {
            List<UsercenterPendingViewModel> result = new List<UsercenterPendingViewModel>();
            //訂單 ( 該會員ID 且 訂單狀態是已付款 且 場地狀態是上架中 )
            var orders = _repository.GetAll<Order>().Where(x => x.MemberID == userid && x.OrderStatusID == 2 && x.Space.SpaceStatusID == 2);
            var reviews = _repository.GetAll<Review>().Where(x => orders.Select(order => order.OrderID).Contains(x.OrderID)).ToList();
            foreach (var order in orders.ToList())
            {
                var resultDetail = new List<RentDetailViewModel>();
                foreach (var orderdetail in order.OrderDetail)
                {
                    var totalhours = (orderdetail.EndDateTime).Subtract(orderdetail.StartDateTime).TotalHours;
                    resultDetail.Add(new RentDetailViewModel
                    {
                        OrderDetailId = orderdetail.OrderDetailID,
                        OrderId = orderdetail.OrderID,
                        RentTime = orderdetail.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = orderdetail.EndDateTime.ToString("yyyy-MM-dd HH:mm"),
                        People = orderdetail.Participants,
                        Money = PayMentService.OrderDetailPrice(totalhours, orderdetail.Order.Space.PricePerHour, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Hour : 1, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Discount : 0),
                    });
                }
                //租借時間的第一天
                var rentTimeFirst = DateTime.Parse(resultDetail.Select(x => x.RentTime).First());
                //租借時間與現在時間差( 總小時數 )
                var rentTimeToNow = rentTimeFirst.Subtract(DateTime.UtcNow.AddHours(8)).TotalHours;
                //轉換成天數 或是 只有小時數
                string renttimedayorhour;
                if (rentTimeToNow <= 24)
                {
                    renttimedayorhour = $"{(int)rentTimeToNow} 小時";
                }
                else
                {
                    renttimedayorhour = $"{(int)rentTimeToNow / 24} 天";
                }
                decimal cancelMoney = 0;
                var totalReviews = reviews.Where(x => x.OrderID == order.OrderID && x.ToHost);
                var score = totalReviews.Any() ? totalReviews.Average(x => x.Score) : 0;
                result.Add(new UsercenterPendingViewModel
                {
                    SpaceId = order.SpaceID,
                    OrderNumber = (int)order.OrderNumber,
                    PaidTime = ((DateTime)order.PaymentDate).ToString("yyyy-MM-dd HH:mm"),
                    SpaceName = order.Space.SpaceName,
                    SpaceUrl = order.Space.SpacePhoto.First().SpacePhotoUrl,
                    OwnerName = order.Space.Member.Name,
                    OwnerPhone = order.Space.Member.Phone,
                    //評分 = 訂單到評分表 找到 場地ID = 訂單場地ID 且 Tohost是True的
                    Score = Average(order.Space.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Count()).Sum(), order.Space.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Sum()).Sum()),
                    TotalMoney = resultDetail.Select(x => x.Money).Sum(),
                    Email = order.Member.Email,
                    OrderId = order.OrderID,
                    CancelTitle = order.Space.Cancellation.CancellationTitle,
                    CancelDetail = order.Space.Cancellation.CancellationDetail,
                    CancelTime = renttimedayorhour,
                    CancelMoney = PayMentService.CancelPrice(order.Space.Cancellation.CancellationID, rentTimeToNow, cancelMoney, resultDetail.Select(x => x.Money).Sum()),
                    OrderStatus = order.OrderStatusID,
                    MemberId = order.MemberID,
                    ContactName = order.ContactName,
                    ContactPhone = order.ContactPhone,
                    RentDetail = resultDetail
                });
            }
            var resultorder = result.OrderByDescending(x => x.PaidTime).ToList();
            return resultorder;
        }
        /// <summary>
        /// 找出使用者(ID)的使用中資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UsercenterProcessingViewModel> GetUsercenterProcessingVM(int userid)
        {
            var result = new List<UsercenterProcessingViewModel>();

            //訂單 ( 該會員ID 且 訂單狀態是使用中 且 場地狀態是上架中 )
            var orders = _repository.GetAll<Order>().Where(x => x.MemberID == userid && x.OrderStatusID == 3 && x.Space.SpaceStatusID == 2);
            var reviews = _repository.GetAll<Review>().Where(x => orders.Select(order => order.OrderID).Contains(x.OrderID)).ToList();
            foreach (var order in orders.ToList())
            {
                var resultDetail = new List<RentDetailViewModel>();
                var totalReviews = reviews.Where(x => x.OrderID == order.OrderID && x.ToHost);
                var score = totalReviews.Any() ? totalReviews.Average(x => x.Score) : 0;
                foreach (var orderdetail in order.OrderDetail)
                {
                    var totalhours = (orderdetail.EndDateTime).Subtract(orderdetail.StartDateTime).TotalHours;
                    resultDetail.Add(new RentDetailViewModel
                    {
                        OrderDetailId = orderdetail.OrderDetailID,
                        OrderId = orderdetail.OrderID,
                        RentTime = orderdetail.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = orderdetail.EndDateTime.ToString("yyyy-MM-dd HH:mm"),
                        People = orderdetail.Participants,
                        Money = PayMentService.OrderDetailPrice(totalhours, orderdetail.Order.Space.PricePerHour, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Hour : 1, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Discount : 0),
                    });
                }
                result.Add(new UsercenterProcessingViewModel
                {
                    SpaceId = order.SpaceID,
                    OrderNumber = (int)order.OrderNumber,
                    PaidTime = ((DateTime)order.PaymentDate).ToString("yyyy-MM-dd HH:mm"),
                    SpaceName = order.Space.SpaceName,
                    SpaceUrl = order.Space.SpacePhoto.First().SpacePhotoUrl,
                    OwnerName = order.Space.Member.Name,
                    OwnerPhone = order.Space.Member.Phone,
                    //評分 = 訂單到評分表 找到 場地ID = 訂單場地ID 且 Tohost是True的
                    Score = Average(order.Space.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Count()).Sum(), order.Space.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Sum()).Sum()),
                    TotalMoney = resultDetail.Select(x => x.Money).Sum(),
                    Email = order.Member.Email,
                    OrderId = order.OrderID,
                    RentDetail = resultDetail
                });
            }
            var resultorder = result.OrderByDescending(x => x.PaidTime).ToList();
            return resultorder;
        }
        /// <summary>
        /// 找出使用者(ID)的已完成資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UsercenterCompletedViewModel> GetUsercenterCompletedVM(int userid)
        {
            List<UsercenterCompletedViewModel> result = new List<UsercenterCompletedViewModel>();

            //訂單 ( 該會員ID 且 訂單狀態是已完成OR已取消 且 場地狀態是上架中 )
            var orders = _repository.GetAll<Order>().Where(x => x.MemberID == userid && x.OrderStatusID == 4 || x.MemberID == userid && x.OrderStatusID == 5 && x.Space.SpaceStatusID == 2);
            var reviews = _repository.GetAll<Review>().Where(x => orders.Select(order => order.OrderID).Contains(x.OrderID)).ToList();
            foreach (var order in orders.ToList())
            {
                var resultDetail = new List<RentDetailViewModel>();
                foreach (var orderdetail in order.OrderDetail)
                {
                    var totalhours = (orderdetail.EndDateTime).Subtract(orderdetail.StartDateTime).TotalHours;

                    resultDetail.Add(new RentDetailViewModel
                    {
                        OrderDetailId = orderdetail.OrderDetailID,
                        OrderId = orderdetail.OrderID,
                        RentTime = orderdetail.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = orderdetail.EndDateTime.ToString("yyyy-MM-dd HH:mm"),
                        People = orderdetail.Participants,
                        Money = PayMentService.OrderDetailPrice(totalhours, orderdetail.Order.Space.PricePerHour, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Hour : 1, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Discount : 0),
                    });
                }
                // 是否有評價過
                bool hasReview = false;
                // 訂單狀態字串
                string orderstaus;
                if (order.OrderStatusID == 4)
                {
                    hasReview = reviews.Where(x => x.ToHost).Any(x => x.OrderID == order.OrderID);
                    orderstaus = "訂單已完成";
                }
                else
                {
                    orderstaus = "訂單已取消";
                }
                var totalReviews = reviews.Where(x => x.OrderID == order.OrderID && x.ToHost);
                var score = totalReviews.Any() ? totalReviews.Average(x => x.Score) : 0;
                result.Add(new UsercenterCompletedViewModel
                {
                    SpaceId = order.SpaceID,
                    OrderNumber = (int)order.OrderNumber,
                    PaidTime = ((DateTime)order.PaymentDate).ToString("yyyy-MM-dd HH:mm"),
                    SpaceName = order.Space.SpaceName,
                    SpaceUrl = order.Space.SpacePhoto.First().SpacePhotoUrl,
                    OwnerName = order.Space.Member.Name,
                    OwnerPhone = order.Space.Member.Phone,
                    //評分 = 訂單到評分表 找到 場地ID = 訂單場地ID 且 Tohost是True的
                    Score = Average(order.Space.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Count()).Sum(), order.Space.Order.Select(x => x.Review.Where(z => z.ToHost == true).Select(y => y.Score).Sum()).Sum()),
                    TotalMoney = resultDetail.Select(x => x.Money).Sum(),
                    Email = order.Member.Email,
                    OrderId = order.OrderID,
                    RentDetail = resultDetail,
                    OrederStatus = orderstaus,
                    HasReview = hasReview,
                });
            }
            var resultorder = result.OrderByDescending(x => x.PaidTime).ToList();
            return resultorder;
        }
        /// <summary>
        /// 刪除已付款的訂單(Nick)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void DeletePendingOrder(UsercenterPendingViewModel model)
        {
            var order = _repository.GetAll<Order>().FirstOrDefault(x => x.OrderID == model.OrderId && x.MemberID == model.MemberId);
            if(order != null)
            {
                order.OrderStatusID = 5;
                order.CancelDateTime = DateTime.UtcNow.AddHours(8);
                try
                {
                    _repository.Update(order);
                    _repository.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new NotImplementedException();
                }
            }

        }

        /// <summary>
        /// 取得訂單使用中資訊 (Jack)
        /// </summary>
        /// <returns></returns>
        public List<ProcessingViewModel> GetHostCenter(int id) 
        {
            var result = new List<ProcessingViewModel>();
            var Orders = _repository.GetAll<Order>().Where(x=> x.OrderStatusID == 3 && x.Space.MemberID == id);
            foreach (var order in Orders) 
            {
                var resultdetail = new ProcessingViewModel{ orderdetailesforprcess = new List<OrderDetailesforPrcess>() };
                foreach (var o in order.OrderDetail)
                {
                    resultdetail.orderdetailesforprcess.Add(new OrderDetailesforPrcess
                    {
                        StratTime = o.StartDateTime,
                        EndTime = o.EndDateTime,
                        People = o.Participants,
                        SinglePrice = PayMentService.OrderDetailPrice(o.EndDateTime.Subtract(o.StartDateTime).TotalHours,o.Order.Space.PricePerHour, o.Order.Space.SpaceDiscount.Any() ? o.Order.Space.SpaceDiscount.First().Hour : 1,o.Order.Space.SpaceDiscount.Any() ? o.Order.Space.SpaceDiscount.First().Discount : 0)
                    });
                }
                result.Add(new ProcessingViewModel
                {
                    SpaceId = order.SpaceID,
                    OrderId = (int)order.OrderNumber,
                    OrderName = order.Member.Name,
                    OrderEmail = order.Member.Email,
                    ContactName = order.ContactName,
                    ContactPhone = order.ContactPhone,
                    SpaceName = order.Space.SpaceName,
                    SpacePhoto = order.Space.SpacePhoto.First().SpacePhotoUrl,
                    orderdetailesforprcess = resultdetail.orderdetailesforprcess,
                    Total = resultdetail.orderdetailesforprcess.Select(x => x.SinglePrice).Sum()
                });
            }
            return result;
        }

        /// <summary>
        /// <summary>
        /// 找出場地主(ID)的歷史訂單使用中資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<HostCenterHistoryViewModel> GetHostCenterHistoryVM(int userid)
        {
            var result = new List<HostCenterHistoryViewModel>();
            //訂單 ( 該會員ID 且 訂單狀態是已完成 且 場地狀態是上架中 )
            var orders = _repository.GetAll<Order>().Where(x => x.Space.MemberID == userid && x.OrderStatusID == 4 );
            var reviews = _repository.GetAll<Review>().Where(x => orders.Select(order => order.OrderID).Contains(x.OrderID)).ToList();
            foreach (var order in orders.ToList())
            {
                var resultDetail = new List<RentDetailViewModel>();
                foreach (var orderdetail in order.OrderDetail)
                {
                    var totalhours = (orderdetail.EndDateTime).Subtract(orderdetail.StartDateTime).TotalHours;
                    resultDetail.Add(new RentDetailViewModel
                    {
                        OrderDetailId = orderdetail.OrderDetailID,
                        OrderId = orderdetail.OrderID,
                        RentTime = orderdetail.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = orderdetail.EndDateTime.ToString("yyyy-MM-dd HH:mm"),
                        People = orderdetail.Participants,
                        Money = PayMentService.OrderDetailPrice(totalhours, orderdetail.Order.Space.PricePerHour, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Hour : 1, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Discount : 0),
                    });
                }
                //是否評價過
                var hasReview = reviews.Where(x => x.ToHost == false).Any(x => x.OrderID == order.OrderID);
                var totalReviews = reviews.Where(x => x.OrderID == order.OrderID && x.ToHost);
                var score = totalReviews.Any() ? totalReviews.Average(x => x.Score) : 0;
                result.Add(new HostCenterHistoryViewModel
                {
                    SpaceId = order.SpaceID,
                    OrderNumber = (int)order.OrderNumber,
                    PaidTime = ((DateTime)order.PaymentDate).ToString("yyyy-MM-dd HH:mm"),
                    SpaceName = order.Space.SpaceName,
                    SpaceUrl = order.Space.SpacePhoto.First().SpacePhotoUrl,
                    OwnerName = order.Space.Member.Name,
                    OwnerPhone = order.Space.Member.Phone,
                    //評分 = 訂單到評分表 找到 場地ID = 訂單場地ID 且 Tohost是True的
                    Score = score,
                    TotalMoney = resultDetail.Select(x => x.Money).Sum(),
                    Email = order.Member.Email,
                    OrderId = order.OrderID,
                    RentDetail = resultDetail,
                    UserName = order.Member.Name,
                    ContactName = order.ContactName,
                    ContactPhone = order.ContactPhone,
                    HasReview = hasReview
                });
            }
            return result;
        }

        /// <summary>
        /// History篩選 (Jack)
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<HostCenterHistoryViewModel> GetHostCenterHistoryVM(HostCenterHistoryViewModel model, int userid)
        {
            var a = (model.SpaceName == null ? 0 : 1).ToString();
            var b = (model.SearchDateTime == null ? 0 : 1).ToString();
            var c = (model.UserName == null ? 0 : 1).ToString();

            var Key = a+b+c;
            var searchkey = new SearchData();
            var result = new List<HostCenterHistoryViewModel>();
            if (int.Parse(a) > 0) 
            { 
                searchkey.SpaceName = model.SpaceName.Replace(" ","");
            }
            if (int.Parse(c) > 0) { 
                searchkey.UserName = model.UserName.Replace(" ","");
            }
            //var sercharcdata = model.SearchDateTime.Any() ? model.SearchDateTime : null;
            //searchkey.SearchDateTime = DateTime.Parse(sercharcdata);
            var orders = _repository.GetAll<Order>().Where(x => x.Space.MemberID == userid && x.OrderStatusID == 4);
            var reviews = _repository.GetAll<Review>().Where(x => orders.Select(order => order.OrderID).Contains(x.OrderID)).ToList();
            var OrderDetails = _repository.GetAll<OrderDetail>().Where(x=>orders.Select(o=>o.OrderID).Contains(x.OrderID));
            if (model.SearchDateTime.Any()) 
            { 
                searchkey.SearchDateTime = DateTime.Parse(model.SearchDateTime);
            }

            switch (Key)//switch (比對的運算式)
            {
                case "000" ://狀況一走這個
                    result = SearchDate(orders,reviews);
                    break;
                case "100":
                    var os100 = orders.Where(x => x.Space.SpaceName == searchkey.SpaceName);
                    //訂單(該會員ID 且 訂單狀態是已完成 且 場地狀態是上架中)
                    var rs100 = reviews.Where(x => os100.Select(o => o.OrderID).Contains(x.OrderID));
                    result = SearchDate( os100, rs100);
                    break;
                case "101":
                    var os101 = orders.Where(x => x.Space.SpaceName == searchkey.SpaceName && x.Member.Name == searchkey.UserName);
                    //訂單(該會員ID 且 訂單狀態是已完成 且 場地狀態是上架中)
                    var rs101 = _repository.GetAll<Review>().Where(x => os101.Select(o => o.OrderID).Contains(x.OrderID)).ToList();
                    result = SearchDate(os101,rs101);
                    break;
                case "001":
                    var os001 = orders.Where(x => x.Member.Name == searchkey.UserName);
                    //訂單(該會員ID 且 訂單狀態是已完成 且 場地狀態是上架中)
                    var rs001 = _repository.GetAll<Review>().Where(x => os001.Select(o => o.OrderID).Contains(x.OrderID)).ToList();
                    result = SearchDate(os001, rs001);
                    break;
                case "010":
                    var od010 = OrderDetails.Where(x => (int)SqlFunctions.DateDiff("day", x.StartDateTime,searchkey.SearchDateTime) <= 1 && (int)SqlFunctions.DateDiff("day", x.StartDateTime, searchkey.SearchDateTime) >= 0).Select(x=>x.OrderID).ToList();
                    var os010 = orders.Where(x=> od010.Contains(x.OrderID));
                    //訂單(該會員ID 且 訂單狀態是已完成 且 場地狀態是上架中)
                    var rs010 = _repository.GetAll<Review>().Where(x => os010.Select(os => os.OrderID).Contains(x.OrderID));
                    result = SearchDate(os010,rs010);
                    break;
                case "110":
                    var od110 = OrderDetails.Where(x => (int)SqlFunctions.DateDiff("day", x.StartDateTime, searchkey.SearchDateTime) <= 1 && (int)SqlFunctions.DateDiff("day", x.StartDateTime, searchkey.SearchDateTime) >= 0).Select(x => x.OrderID).ToList();
                    var os110 = orders.Where(x => od110.Contains(x.OrderID) && x.Space.Member.Name == searchkey.UserName);
                    //result = SearchDate(os010,rs010);(x => x.Space.SpaceName == searchkey.SpaceName && x.OrderDetail.Select(od=>od.StartDateTime).Contains(searchkey.SearchDateTime));
                    //訂單(該會員ID 且 訂單狀態是已完成 且 場地狀態是上架中)
                    var rs110 = _repository.GetAll<Review>().Where(x => os110.Select(o => o.OrderID).Contains(x.OrderID)).ToList();
                    result = SearchDate(os110,rs110);
                    break;
                case "011":
                    var od011 = OrderDetails.Where(x => (int)SqlFunctions.DateDiff("day", x.StartDateTime, searchkey.SearchDateTime) <= 1 && (int)SqlFunctions.DateDiff("day", x.StartDateTime, searchkey.SearchDateTime) >= 0).Select(x => x.OrderID).ToList();
                    var os011 = orders.Where(x => od011.Contains(x.OrderID) && x.Member.Name == searchkey.UserName);
                    //訂單(該會員ID 且 訂單狀態是已完成 且 場地狀態是上架中)
                    var rs011 = _repository.GetAll<Review>().Where(x => os011.Select(o => o.OrderID).Contains(x.OrderID)).ToList();
                    result = SearchDate(os011,rs011);
                    break;
                case "111":
                    var od111 = OrderDetails.Where(x => (int)SqlFunctions.DateDiff("day", x.StartDateTime, searchkey.SearchDateTime) <= 1 && (int)SqlFunctions.DateDiff("day", x.StartDateTime, searchkey.SearchDateTime) >= 0).Select(x => x.OrderID).ToList();
                    var os111 = orders.Where(x => od111.Contains(x.OrderID) && x.Member.Name == searchkey.UserName && x.Space.SpaceName == searchkey.SpaceName);
                    //訂單(該會員ID 且 訂單狀態是已完成 且 場地狀態是上架中)
                    var rs111 = _repository.GetAll<Review>().Where(x => os111.Select(o => o.OrderID).Contains(x.OrderID)).ToList();
                    result = SearchDate(os111, rs111);
                    break;
                default://以上都不符合走這個
                    break;
            }

            return result;
        }
        
        /// <summary>
        /// 配合History篩選Switch Case() (Jack) 
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="reviews"></param>
        /// <returns></returns>
        private static List<HostCenterHistoryViewModel> SearchDate(IQueryable<Order> orders, IEnumerable<Review> reviews)
        {
            var result = new List<HostCenterHistoryViewModel>();
            foreach (var order in orders.ToList())
            {
                var resultDetail = new List<RentDetailViewModel>();
                foreach (var orderdetail in order.OrderDetail)
                {
                    var totalhours = (orderdetail.EndDateTime).Subtract(orderdetail.StartDateTime).TotalHours;
                    resultDetail.Add(new RentDetailViewModel
                    {
                        OrderDetailId = orderdetail.OrderDetailID,
                        OrderId = orderdetail.OrderID,
                        RentTime = orderdetail.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = orderdetail.EndDateTime.ToString("yyyy-MM-dd HH:mm"),
                        People = orderdetail.Participants,
                        Money = PayMentService.OrderDetailPrice(totalhours, orderdetail.Order.Space.PricePerHour, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Hour : 1, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Discount : 0),
                    });
                }
                //是否評價過
                var hasReview = reviews.Where(x => x.ToHost == false).Any(x => x.OrderID == order.OrderID);
                var totalReviews = reviews.Where(x => x.OrderID == order.OrderID && x.ToHost);
                var score = totalReviews.Any() ? totalReviews.Average(x => x.Score) : 0;
                result.Add(new HostCenterHistoryViewModel
                {
                    SpaceId = order.SpaceID,
                    OrderNumber = (int)order.OrderNumber,
                    PaidTime = ((DateTime)order.PaymentDate).ToString("yyyy-MM-dd HH:mm"),
                    SpaceName = order.Space.SpaceName,
                    SpaceUrl = order.Space.SpacePhoto.First().SpacePhotoUrl,
                    OwnerName = order.Space.Member.Name,
                    OwnerPhone = order.Space.Member.Phone,
                    //評分 = 訂單到評分表 找到 場地ID = 訂單場地ID 且 Tohost是True的
                    Score = score,
                    TotalMoney = resultDetail.Select(x => x.Money).Sum(),
                    Email = order.Member.Email,
                    OrderId = order.OrderID,
                    RentDetail = resultDetail,
                    UserName = order.Member.Name,
                    ContactName = order.ContactName,
                    ContactPhone = order.ContactPhone,
                    HasReview = hasReview
                });
            }
            return result;
        }

        /// <summary>
        /// 平均分數 (Jack)
        /// </summary>
        /// <param name="count"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        private static double Average(int count, int score)
        {
            double result = 0;
            if (count == 0 || score == 0)
            {
                result = 0;
            }
            else
            {
                result = score / count;
            }
            return result;
        }
        /// <summary>
        /// 搜尋方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public List<HostCenterHistoryViewModel> GetHistoryData(Order orders)
        //{
        //    var result = new List<HostCenterHistoryViewModel>();
        //    //訂單(該會員ID 且 訂單狀態是已完成 且 場地狀態是上架中)
        //    var orders = _repository.GetAll<Order>().Where(x => x.Space.MemberID == userid && x.OrderStatusID == 4);
        //    var reviews = _repository.GetAll<Review>().Where(x => orders.Select(order => order.OrderID).Contains(x.OrderID)).ToList();
        //    foreach (var order in orders.ToList())
        //    {
        //        var resultDetail = new List<RentDetailViewModel>();
        //        foreach (var orderdetail in order.OrderDetail)
        //        {
        //            var totalhours = (orderdetail.EndDateTime).Subtract(orderdetail.StartDateTime).TotalHours;
        //            resultDetail.Add(new RentDetailViewModel
        //            {
        //                OrderDetailId = orderdetail.OrderDetailID,
        //                OrderId = orderdetail.OrderID,
        //                RentTime = orderdetail.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
        //                RentBackTime = orderdetail.EndDateTime.ToString("yyyy-MM-dd HH:mm"),
        //                People = orderdetail.Participants,
        //                Money = PayMentService.OrderDetailPrice(totalhours, orderdetail.Order.Space.PricePerHour, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Hour : 1, orderdetail.Order.Space.SpaceDiscount.Any() ? orderdetail.Order.Space.SpaceDiscount.First().Discount : 0),
        //            });
        //        }
        //        //是否評價過
        //        var hasReview = reviews.Where(x => x.ToHost == false).Any(x => x.OrderID == order.OrderID);
        //        var totalReviews = reviews.Where(x => x.OrderID == order.OrderID && x.ToHost);
        //        var score = totalReviews.Any() ? totalReviews.Average(x => x.Score) : 0;
        //        result.Add(new HostCenterHistoryViewModel
        //        {
        //            SpaceId = order.SpaceID,
        //            OrderNumber = (int)order.OrderNumber,
        //            PaidTime = ((DateTime)order.PaymentDate).ToString("yyyy-MM-dd HH:mm"),
        //            SpaceName = order.Space.SpaceName,
        //            SpaceUrl = order.Space.SpacePhoto.First().SpacePhotoUrl,
        //            OwnerName = order.Space.Member.Name,
        //            OwnerPhone = order.Space.Member.Phone,
        //            //評分 = 訂單到評分表 找到 場地ID = 訂單場地ID 且 Tohost是True的
        //            Score = score,
        //            TotalMoney = resultDetail.Select(x => x.Money).Sum(),
        //            Email = order.Member.Email,
        //            OrderId = order.OrderID,
        //            RentDetail = resultDetail,
        //            UserName = order.Member.Name,
        //            ContactName = order.ContactName,
        //            ContactPhone = order.ContactPhone,
        //            HasReview = hasReview
        //        });
        //    }
        //    return result;
        //}
        public class SearchData
        {
            public string SpaceName { get; set; }
            public DateTime SearchDateTime { get; set; }
            public string UserName { get; set; }
        }
    }
}