using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
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
        public enum CancellationOptions
        {
            VeryFlexible = 1,
            Flexible = 2,
            StandardThirtyDays = 3,
            StandardNintyDays = 4
        }
        /// <summary>
        /// 找出使用者(ID)的已付款資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UsercenterPendingViewModel> GetUsercenterPendingVM(int userid)
        {
            List<UsercenterPendingViewModel> result = new List<UsercenterPendingViewModel>();
            var resultDetail = new UsercenterPendingViewModel
            {
                RentDetail = new List<RentDetailViewModel>()
            };
            //訂單 ( 該會員ID 且 訂單狀態是已付款 且 場地狀態是上架中 )
            var orders = _repository.GetAll<Order>().Where(x => x.MemberID == userid && x.OrderStatusID == 2 && x.Space.SpaceStatusID == 2);
            var reviews = _repository.GetAll<Review>();
            foreach(var order in orders)
            {
                foreach(var orderdetail in order.OrderDetail)
                {
                    resultDetail.RentDetail.Add(new RentDetailViewModel
                    {
                        OrderDetailId = orderdetail.OrderDetailID,
                        OrderId = orderdetail.OrderID,
                        RentTime = orderdetail.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = orderdetail.EndDateTime.ToString("yyyy-MM-dd HH:mm"),
                        People = orderdetail.Participants,
                        Money = PayMentService.OrderDetailPrice(orderdetail.EndDateTime, orderdetail.StartDateTime, orderdetail.Order.Space.PricePerHour, orderdetail.Order.Space.SpaceDiscount.First().Hour, orderdetail.Order.Space.SpaceDiscount.First().Discount),
                    });
                }

                //租借時間的第一天
                var rentTimeFirst =DateTime.Parse(resultDetail.RentDetail.Select(x => x.RentTime).First());
                //租借時間與現在時間差( 總小時數 )
                var rentTimeToNow = rentTimeFirst.Subtract(DateTime.Now).TotalHours;
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
                    Score = reviews.Where(x => x.Order.SpaceID == order.SpaceID && x.ToHost).Select(x => x.Score).Average(),
                    TotalMoney = resultDetail.RentDetail.Select(x => x.Money).Sum(),
                    Email = order.Member.Email,
                    OrderId = order.OrderID,
                    CancelTitle = order.Space.Cancellation.CancellationTitle,
                    CancelDetail = order.Space.Cancellation.CancellationDetail,
                    CancelTime = renttimedayorhour,
                    CancelMoney = PayMentService.CancelPrice(order.Space.Cancellation.CancellationID , rentTimeToNow , cancelMoney , resultDetail.RentDetail.Select(x => x.Money).Sum()),
                    OrderStatus = order.OrderStatusID,
                    MemberId = order.MemberID,
                    ContactName = order.ContactName,
                    ContactPhone = order.ContactPhone,
                    RentDetail = resultDetail.RentDetail
                });
            }
            return result;
        }
        /// <summary>
        /// 找出使用者(ID)的使用中資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<OrderViewModel> GetUsercenterProcessingVM(int userid)
        {
            var result = new List<OrderViewModel>();
            var resultDetail = new OrderViewModel
            {
                RentDetail = new List<RentDetailViewModel>()
            };
            //訂單 ( 該會員ID 且 訂單狀態是使用中 且 場地狀態是上架中 )
            var orders = _repository.GetAll<Order>().Where(x => x.MemberID == userid && x.OrderStatusID == 3 && x.Space.SpaceStatusID == 2);
            var reviews = _repository.GetAll<Review>();
            foreach (var order in orders)
            {
                foreach (var orderdetail in order.OrderDetail)
                {
                    resultDetail.RentDetail.Add(new RentDetailViewModel
                    {
                        OrderDetailId = orderdetail.OrderDetailID,
                        OrderId = orderdetail.OrderID,
                        RentTime = orderdetail.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = orderdetail.EndDateTime.ToString("yyyy-MM-dd HH:mm"),
                        People = orderdetail.Participants,
                        Money = PayMentService.OrderDetailPrice(orderdetail.EndDateTime, orderdetail.StartDateTime, orderdetail.Order.Space.PricePerHour, orderdetail.Order.Space.SpaceDiscount.First().Hour, orderdetail.Order.Space.SpaceDiscount.First().Discount),
                    });
                }
                result.Add(new OrderViewModel
                {
                    SpaceId = order.SpaceID,
                    OrderNumber = (int)order.OrderNumber,
                    PaidTime = ((DateTime)order.PaymentDate).ToString("yyyy-MM-dd HH:mm"),
                    SpaceName = order.Space.SpaceName,
                    SpaceUrl = order.Space.SpacePhoto.First().SpacePhotoUrl,
                    OwnerName = order.Space.Member.Name,
                    OwnerPhone = order.Space.Member.Phone,
                    //評分 = 訂單到評分表 找到 場地ID = 訂單場地ID 且 Tohost是True的
                    Score = reviews.Where(x => x.Order.SpaceID == order.SpaceID && x.ToHost).Select(x => x.Score).Average(),
                    TotalMoney = resultDetail.RentDetail.Select(x => x.Money).Sum(),
                    Email = order.Member.Email,
                    OrderId = order.OrderID,
                    RentDetail = resultDetail.RentDetail
                });
            }
            return result;
        }
        /// <summary>
        /// 找出使用者(ID)的已完成資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UsercenterCompletedViewModel> GetUsercenterCompletedVM(int userid)
        {
            List<UsercenterCompletedViewModel> result = new List<UsercenterCompletedViewModel>();
            var resultDetail = new UsercenterCompletedViewModel
            {
                RentDetail = new List<RentDetailViewModel>()
            };
            //訂單 ( 該會員ID 且 訂單狀態是已完成OR已取消 且 場地狀態是上架中 )
            var orders = _repository.GetAll<Order>().Where(x => x.MemberID == userid && x.OrderStatusID == 4 || x.MemberID == userid && x.OrderStatusID == 5 && x.Space.SpaceStatusID == 2);
            var reviews = _repository.GetAll<Review>();
            foreach(var order in orders)
            {
                foreach (var orderdetail in order.OrderDetail)
                {
                    resultDetail.RentDetail.Add(new RentDetailViewModel
                    {
                        OrderDetailId = orderdetail.OrderDetailID,
                        OrderId = orderdetail.OrderID,
                        RentTime = orderdetail.StartDateTime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = orderdetail.EndDateTime.ToString("yyyy-MM-dd HH:mm"),
                        People = orderdetail.Participants,
                        Money = PayMentService.OrderDetailPrice(orderdetail.EndDateTime, orderdetail.StartDateTime, orderdetail.Order.Space.PricePerHour, orderdetail.Order.Space.SpaceDiscount.First().Hour, orderdetail.Order.Space.SpaceDiscount.First().Discount),
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
                    Score = reviews.Where(x => x.Order.SpaceID == order.SpaceID && x.ToHost).Select(x => x.Score).Average(),
                    TotalMoney = resultDetail.RentDetail.Select(x => x.Money).Sum(),
                    Email = order.Member.Email,
                    OrderId = order.OrderID,
                    RentDetail = resultDetail.RentDetail,
                    OrederStatus = orderstaus,
                    HasReview = hasReview,
                });
            }
            return result;
        }
        /// <summary>
        /// 刪除已付款的訂單(Nick)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Order DeletePendingOrder(UsercenterPendingViewModel model)
        {
            var order = new Order {
                OrderID = model.OrderId,
                OrderNumber = model.OrderNumber,
                MemberID = model.MemberId,
                PaymentDate = DateTime.Parse(model.PaidTime),
                ContactName = model.ContactName,
                ContactPhone = model.ContactPhone,
                OrderStatusID = 5
            };

            _repository.Update<Order>(order);
            _repository.SaveChanges();

            return order;
        }

        /// <summary>
        /// 取得訂單使用中資訊 (Jack)
        /// </summary>
        /// <returns></returns>
        public List<ProcessingViewModel> GetHostCenter(int id) 
        {
            var result = new List<ProcessingViewModel>();
            var resultdetail = new ProcessingViewModel 
            {
                orderdetailesforprcess = new List<OrderDetailesforPrcess>()
            };
            var Orders = _repository.GetAll<Order>().Where(x=> x.OrderStatusID == 3 && x.Space.MemberID == id);
            
            foreach (var order in Orders) 
            { 
                foreach(var o in order.OrderDetail) 
                { 
                    resultdetail.orderdetailesforprcess.Add(new OrderDetailesforPrcess {
                        StratTime = o.StartDateTime,
                        EndTime =  o.EndDateTime,
                        People = o.Participants,
                        SinglePrice = (int)SingleOrderDetailPrice(o.EndDateTime,o.StartDateTime,o.Order.Space.PricePerHour,o.Order.Space.SpaceDiscount.FirstOrDefault().Hour, o.Order.Space.SpaceDiscount.FirstOrDefault().Discount)
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
        /// 計算價錢場地 (Jack)
        /// </summary>
        /// <returns></returns>
        public decimal SingleOrderDetailPrice(DateTime eDate,DateTime sDate,decimal hourPirce,int hour,decimal discount) 
        {
            decimal dis = 0;
            if ( (int)eDate.Subtract(sDate).TotalHours >= hour )
            {
                dis = discount;
            }
            else 
            {
                dis = 0;
            }
            var price = (decimal)eDate.Subtract(sDate).TotalHours * hourPirce*(1-dis);
            return price;
        }
        /// <summary>
        /// 找出場地主(ID)的歷史訂單使用中資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<HostCenterHistoryViewModel> GetHostCenterHistoryVM(int id)
        {
            var result = new List<HostCenterHistoryViewModel>();

            var spaces = _repository.GetAll<Space>().ToList();
            var orders = _repository.GetAll<Order>().ToList();
            var members = _repository.GetAll<Member>().ToList();
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var spacepics = _repository.GetAll<SpacePhoto>().ToList();
            var spacediscounts = _repository.GetAll<SpaceDiscount>().ToList();
            var reviews = _repository.GetAll<Review>().ToList();

            //帶入會員ID
            var orderformember = orders.Where(x => x.Space.MemberID == id && x.OrderStatusID == 4);

            foreach(var item in orderformember)
            {
                //場地名稱
                var spacename = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpaceName;
                //場地圖片
                var spacepic = spacepics.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpacePhotoUrl;
                //活動主名稱
                var username = members.FirstOrDefault(x => x.MemberID == item.MemberID).Name;
                //活動主Email
                var email = members.FirstOrDefault(x => x.MemberID == item.MemberID).Email;
                //是否評價過
                var hasReview = reviews.Where(x => x.ToHost == false).Any(x => x.OrderID == item.OrderID);

                decimal money = 0;
                var temp = new List<RentDetailViewModel>();
                //訂單時間 + 人數
                foreach(var rentdetail in item.OrderDetail)
                {
                    var renttime = rentdetail.StartDateTime;
                    var rentbacktime = rentdetail.EndDateTime;
                    var people = rentdetail.Participants;
                    var detailid = rentdetail.OrderDetailID;

                    //算金額
                    var totalhour = rentbacktime.Subtract(renttime).TotalHours;
                    var discounthour = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Hour;
                    var discount = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Discount;
                    var spacehourofmoney = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).PricePerHour;
                    decimal moneytemp;
                    if (totalhour > discounthour)
                    {
                        moneytemp = (spacehourofmoney * (int)totalhour) * (1 - discount);
                    }
                    else
                    {
                        moneytemp = spacehourofmoney * (int)totalhour;
                    }

                    money += moneytemp;
                    moneytemp = 0;
                    temp.Add(new RentDetailViewModel
                    {
                        RentTime = renttime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = rentbacktime.ToString("yyyy-MM-dd HH:mm"),
                        People = people,
                        OrderDetailId = detailid,
                        OrderId = rentdetail.OrderID
                    });
                }

                result.Add(new HostCenterHistoryViewModel
                {
                    SpaceName = spacename,
                    SpaceUrl = spacepic,
                    UserName = username,
                    ContactName = item.ContactName,
                    ContactPhone = item.ContactPhone,
                    TotalMoney = money,
                    OrderId = item.OrderID,
                    SpaceId = item.SpaceID,
                    OrderNumber = (int)item.OrderNumber,
                    RentDetail = temp,
                    Email = email,
                    HasReview = hasReview
                });
            }

            return result;
        }
    }
}