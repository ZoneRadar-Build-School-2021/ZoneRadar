using System;
using System.Collections.Generic;
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
        /// <summary>
        /// 找出使用者(ID)的已付款資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UsercenterPendingViewModel> GetUsercenterPendingVM(int id)
        {
            List<UsercenterPendingViewModel> UCPendingList = new List<UsercenterPendingViewModel>();

            var spaces = _repository.GetAll<Space>().ToList();
            var orders = _repository.GetAll<Order>().ToList();
            var members = _repository.GetAll<Member>().ToList();
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var spacepics = _repository.GetAll<SpacePhoto>().ToList();
            var reviews = _repository.GetAll<Review>().ToList();
            var cancels = _repository.GetAll<Cancellation>().ToList();
            var spacediscounts = _repository.GetAll<SpaceDiscount>().ToList();

            var orderformember = orders.Where(x => x.MemberID == id && x.OrderStatusID == 2);


            foreach (var item in orderformember)
            {

                var ordernum = item.OrderNumber;
                var ownerid = item.Space.MemberID;
                var email = members.FirstOrDefault(x => x.MemberID == ownerid).Email;
                var paidtime = item.PaymentDate;
                var score = reviews.Where(x => x.Order.SpaceID == item.SpaceID && x.ToHost == true).Select(x => x.Score).Average();
                var spacename = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpaceName;
                var spaceurl = spacepics.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpacePhotoUrl;
                var renttime = orderdetails.Where(x => x.OrderID == item.OrderID).Select(x => x.StartDateTime).FirstOrDefault();
                var rentbacktime = orderdetails.Where(x => x.OrderID == item.OrderID).Select(x => x.EndDateTime).FirstOrDefault();
                var people = orderdetails.Where(x => x.OrderID == item.OrderID).Select(x => x.Participants).FirstOrDefault();

                //算金額
                var totalhour = rentbacktime.Subtract(renttime).TotalHours;
                var discounthour = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Hour;
                var discount = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Discount;
                var spacehourofmoney = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).PricePerHour;
                decimal money = 0;
                if (totalhour > discounthour)
                {
                    money = (spacehourofmoney * (int)totalhour) * (1 - discount);
                }
                else
                {
                    money = spacehourofmoney * (int)totalhour;
                }
                //退款政策
                var canceltitle = cancels.FirstOrDefault(x => x.CancellationID == item.Space.CancellationID).CancellationTitle;
                var canceldetail = cancels.FirstOrDefault(x => x.CancellationID == item.Space.CancellationID).CancellationDetail;

                //算退款
                decimal cancelmoney = 0;
                var cancelid = cancels.FirstOrDefault(x => x.CancellationID == item.Space.CancellationID).CancellationID;
                //租借時間距離現在時間差：
                var renttimetonow = renttime.Subtract(DateTime.Now).TotalHours;
                //時間轉天
                string renttimedayorhour;
                if (renttimetonow / 24 <= 24)
                {
                    renttimedayorhour = $"{(int)renttimetonow / 24} 天";
                }
                else
                {
                    renttimedayorhour = $"{(int)renttimetonow} 小時";
                }
                //退錢判斷
                if (cancelid == 1)
                {
                    if (renttimetonow <= 24)
                    {
                        cancelmoney = 0;
                    }
                    else
                    {
                        cancelmoney = money;
                    }
                }
                else if (cancelid == 2)
                {
                    if (renttimetonow <= 24)
                    {
                        cancelmoney = 0;
                    }
                    else if (renttimetonow > 24 && renttimetonow <= (24 * 7))
                    {
                        cancelmoney = money / 2;
                    }
                    else
                    {
                        cancelmoney = money;
                    }
                }
                else if (cancelid == 3)
                {
                    if (renttimetonow <= (24 * 7))
                    {
                        cancelmoney = 0;
                    }
                    else if (renttimetonow > (24 * 7) && renttimetonow <= (24 * 30))
                    {
                        cancelmoney = money / 2;
                    }
                    else
                    {
                        cancelmoney = money;
                    }
                }
                else if (cancelid == 4)
                {
                    if (renttimetonow <= (24 * 14))
                    {
                        cancelmoney = 0;
                    }
                    else if (renttimetonow > (24 * 14) && renttimetonow <= (24 * 90))
                    {
                        cancelmoney = money / 2;
                    }
                    else
                    {
                        cancelmoney = money;
                    }
                }


                UCPendingList.Add(new UsercenterPendingViewModel
                {
                    OrderNumber = (int)ordernum,
                    PaidTime = (DateTime)paidtime,
                    Score = (int)score,
                    SpaceName = spacename,
                    SpaceUrl = spaceurl,
                    RentTime = renttime,
                    RentBackTime = rentbacktime,
                    People = people,
                    Money = money,
                    CancelTitle = canceltitle,
                    CancelDetail = canceldetail,
                    CancelTime = renttimedayorhour,
                    CancelMoney = cancelmoney,
                    OrderStatus = item.OrderStatusID,
                    OrderId = item.OrderID,
                    SpaceId = item.SpaceID,
                    MemberId = item.MemberID,
                    ContactName = item.ContactName,
                    ContactPhone = item.ContactPhone,
                    SpaceID = item.SpaceID,
                    Email = email
                });

            }
            return UCPendingList;
        }
        /// <summary>
        /// 找出使用者(ID)的使用中資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UsercenterProcessingViewModel> GetUsercenterProcessingVM(int id)
        {
            List<UsercenterProcessingViewModel> UCProcessingList = new List<UsercenterProcessingViewModel>();

            var spaces = _repository.GetAll<Space>().ToList();
            var orders = _repository.GetAll<Order>().ToList();
            var members = _repository.GetAll<Member>().ToList();
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var spacepics = _repository.GetAll<SpacePhoto>().ToList();
            var reviews = _repository.GetAll<Review>().ToList();
            var spacediscounts = _repository.GetAll<SpaceDiscount>().ToList();

            var orderformember = orders.Where(x => x.MemberID == id && x.OrderStatusID == 3);

            foreach (var item in orderformember)
            {
                var score = reviews.Where(x => x.Order.SpaceID == item.SpaceID && x.ToHost == true).Select(x => x.Score).Average();


                var ownerid = item.Space.MemberID;
                var email = members.FirstOrDefault(x => x.MemberID == ownerid).Email;
                var ordernum = item.OrderNumber;
                var paidtime = item.PaymentDate;
                var spacename = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpaceName;
                var spaceurl = spacepics.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpacePhotoUrl;
                var renttime = orderdetails.Where(x => x.OrderID == item.OrderID).Select(x => x.StartDateTime).FirstOrDefault();
                var rentbacktime = orderdetails.Where(x => x.OrderID == item.OrderID).Select(x => x.EndDateTime).FirstOrDefault();
                var people = orderdetails.Where(x => x.OrderID == item.OrderID).Select(x => x.Participants).FirstOrDefault();

                //算金額
                var totalhour = rentbacktime.Subtract(renttime).TotalHours;
                var discounthour = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Hour;
                var discount = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Discount;
                var spacehourofmoney = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).PricePerHour;
                decimal money = 0;
                if (totalhour > discounthour)
                {
                    money = (spacehourofmoney * (int)totalhour) * (1 - discount);
                }
                else
                {
                    money = spacehourofmoney * (int)totalhour;
                }

                UCProcessingList.Add(new UsercenterProcessingViewModel
                {
                    OrderNumber = (int)ordernum,
                    PaidTime = (DateTime)paidtime,
                    SpaceName = spacename,
                    Score = (int)score,
                    SpaceUrl = spaceurl,
                    RentTime = renttime,
                    RentBackTime = rentbacktime,
                    People = people,
                    Money = money,
                    SpaceID = item.SpaceID,
                    Email = email
                });
            }
            return UCProcessingList;
        }
        /// <summary>
        /// 找出使用者(ID)的已完成資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UsercenterCompletedViewModel> GetUsercenterCompletedVM(int id)
        {
            List<UsercenterCompletedViewModel> UCCompletedList = new List<UsercenterCompletedViewModel>();

            var spaces = _repository.GetAll<Space>().ToList();
            var orders = _repository.GetAll<Order>().ToList();
            var members = _repository.GetAll<Member>().ToList();
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var spacepics = _repository.GetAll<SpacePhoto>().ToList();
            var spacediscounts = _repository.GetAll<SpaceDiscount>().ToList();
            var views = _repository.GetAll<Review>().ToList();

            var orderformember = orders.Where(x => x.MemberID == id && x.OrderStatusID == 4 || x.MemberID == 19 && x.OrderStatusID == 5);

            foreach (var item in orderformember)
            {
                string orderstaus;
                bool hasReview = false;
                var spaceid = orders.FirstOrDefault(x => x.OrderID == item.OrderID).SpaceID;

                var ownerid = item.Space.MemberID;
                var email = members.FirstOrDefault(x => x.MemberID == ownerid).Email;

                var ordernum = orders.FirstOrDefault(x => x.OrderID == item.OrderID).OrderNumber;
                var orderstatusid = item.OrderStatusID;
                if (orderstatusid == 4)
                {
                    hasReview = views.Where(x => x.ToHost).Any(x => x.OrderID == item.OrderID);
                    orderstaus = "訂單已完成";
                }
                else
                {
                    orderstaus = "訂單已取消";
                }
                var publishtime = orders.FirstOrDefault(x => x.SpaceID == spaceid).PaymentDate;
                var spacename = spaces.FirstOrDefault(x => x.SpaceID == spaceid).SpaceName;
                var spaceurl = spacepics.FirstOrDefault(x => x.SpaceID == spaceid).SpacePhotoUrl;
                var renttime = orderdetails.Where(x => x.OrderID == item.OrderID).Select(x => x.StartDateTime).FirstOrDefault();
                var rentbacktime = orderdetails.Where(x => x.OrderID == item.OrderID).Select(x => x.EndDateTime).FirstOrDefault();
                var people = orderdetails.Where(x => x.OrderID == item.OrderID).Select(x => x.Participants).FirstOrDefault();

                //算金額
                var totalhour = rentbacktime.Subtract(renttime).TotalHours;
                var discounthour = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Hour;
                var discount = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Discount;
                var spacehourofmoney = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).PricePerHour;
                decimal money = 0;
                if (totalhour > discounthour)
                {
                    money = (spacehourofmoney * (int)totalhour) * (1 - discount);
                }
                else
                {
                    money = spacehourofmoney * (int)totalhour;
                }

                //評價表



                UCCompletedList.Add(new UsercenterCompletedViewModel
                {
                    OrderNumber = (int)ordernum,
                    OrederStatus = orderstaus,
                    PublishTime = (DateTime)publishtime,
                    SpaceName = spacename,
                    SpaceUrl = spaceurl,
                    RentTime = renttime,
                    RentBackTime = rentbacktime,
                    People = people,
                    Money = money,
                    HasReview = hasReview,
                    OrderId = item.OrderID,
                    SpaceID = item.SpaceID,
                    Email = email
                });
            }

            return UCCompletedList;
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
                SpaceID = model.SpaceId,
                MemberID = model.MemberId,
                PaymentDate = model.PaidTime,
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
                    Money = money,
                    OrderID = item.OrderID,
                    SpaceID = item.SpaceID,
                    OrderNumber = (int)item.OrderNumber,
                    RentDetailVM = temp,
                    Email = email
                });
            }

            return result;
        }
    }
}