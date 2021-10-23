using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class PreOrderService
    {
        private readonly ZONERadarRepository _repository;
        public PreOrderService()
        {
            _repository = new ZONERadarRepository();
        }
        /// <summary>
        /// 找出使用者(ID)的預購單資料(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<OrderViewModel> GetShopCarVM(int id)
        {
            var result = new List<OrderViewModel>();

            var spaces = _repository.GetAll<Space>().ToList();
            var orders = _repository.GetAll<Order>().ToList();
            var members = _repository.GetAll<Member>().ToList();
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var spacepics = _repository.GetAll<SpacePhoto>().ToList();
            var spacediscounts = _repository.GetAll<SpaceDiscount>().ToList();

            //帶入會員ID
            var orderformember = orders.Where(x => x.MemberID == id && x.OrderStatusID == 1 && x.Space.SpaceStatusID == 2);

            //在所有訂單中符合此會員ID
            foreach (var item in orderformember)
            {
                //該場地名稱
                var spacename = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpaceName;
                //該場地照片
                var spacepic = spacepics.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpacePhotoUrl;
                //該場地主姓名
                var ownername = members.FirstOrDefault(x => x.MemberID == item.Space.MemberID).Name;
                //該場地主電話
                var ownerphone = members.FirstOrDefault(x => x.MemberID == item.Space.MemberID).Phone;

                var ownerid = item.Space.MemberID;
                var email = members.FirstOrDefault(x => x.MemberID == ownerid).Email;

                decimal money = 0;
                var temp = new List<RentDetailViewModel>();
                //訂單時間及人數
                foreach (var rentdetail in item.OrderDetail)
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



                result.Add(new OrderViewModel
                {
                    SpaceName = spacename,
                    SpaceUrl = spacepic,
                    OwnerName = ownername,
                    OwnerPhone = ownerphone,
                    TotalMoney = money,
                    RentDetail = temp,
                    OrderId = item.OrderID,
                    SpaceId = item.SpaceID,
                    Email = email
                });
            }
            return result;
        }
        /// <summary>
        /// 編輯使用者預購單內的訂單細節(Nick)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OrderDetail EditShopCarDetail(RentDetailViewModel model)
        {
            var orderDetail = new OrderDetail();

            orderDetail.OrderID = model.OrderId;
            orderDetail.StartDateTime = DateTime.Parse(model.RentTime);
            orderDetail.EndDateTime = DateTime.Parse(model.RentBackTime);
            orderDetail.OrderDetailID = model.OrderDetailId;
            orderDetail.Participants = model.People;
            if (orderDetail.OrderDetailID == model.OrderDetailId)
            {
                _repository.Update<OrderDetail>(orderDetail);
                _repository.SaveChanges();
            }
            return orderDetail;
        }
        /// <summary>
        /// 刪除使用者預購單內的訂單細節(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderDetail DeleteShopCarDetail(int id)
        {
            //OrderDetail orderdetail = OrderDetail.Find(id);
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var orderdetail = orderdetails.FirstOrDefault(x => x.OrderDetailID == id);


            _repository.Delete<OrderDetail>(orderdetail);
            _repository.SaveChanges();


            return orderdetail;
        }
        /// <summary>
        /// 刪除使用者預購單內的該筆訂單(Nick)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order DeleteShopCarOrder(int id)
        {
            var orders = _repository.GetAll<Order>().ToList();
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var orderdetail = orderdetails.FindAll(x => x.OrderID == id);
            foreach(var item in orderdetail)
            {
                if(item != null)
                {
                    _repository.Delete<OrderDetail>(item);
                    _repository.SaveChanges();
                }
            }
            var order = orders.FirstOrDefault(x => x.OrderID == id);

            _repository.Delete<Order>(order);
            _repository.SaveChanges();

            return order;
        }

        /// <summary>
        /// 預約頁面預購轉成預購單到DB(Steve)
        /// </summary>
        /// <param name="preOrderVM"></param>
        /// <param name="memberID"></param>
        public void PlaceAPreOrder(PreOrderViewModel preOrderVM, int memberID)
        {
            var spaceID = preOrderVM.SpaceID;

            var newOrder = new Order
            {
                SpaceID = spaceID,
                MemberID = memberID,
                ContactName = "123",
                ContactPhone = "123",
                OrderStatusID = 1
            };

            _repository.Create<Order>(newOrder);
            _repository.SaveChanges();

            var orderID = newOrder.OrderID;
            var bookingDateList = preOrderVM.DatesArr;
            var attendeesList = preOrderVM.AttendeesArr;
            var startTimeList = preOrderVM.StartTimeArr;
            var endTimeList = preOrderVM.EndTimeArr;

            var startDateTimeList = new List<DateTime>();
            var endDateTimeList = new List<DateTime>();
            for (int i = 0; i < bookingDateList.Count; i++)
            {
                startDateTimeList.Add(DateTime.Parse($"{bookingDateList[i]}T{startTimeList[i]:00}"));
                endDateTimeList.Add(DateTime.Parse($"{bookingDateList[i]}T{endTimeList[i]:00}"));
            }

            var newOrderDetail = new List<OrderDetail>();
            for (int i = 0; i < startDateTimeList.Count; i++)
            {
                newOrderDetail.Add(new OrderDetail
                {
                    OrderID = orderID,
                    StartDateTime = startDateTimeList[i],
                    EndDateTime = endDateTimeList[i],
                    Participants = int.Parse(attendeesList[i]),
                });
            }

            _repository.CreateRange<OrderDetail>(newOrderDetail);
            _repository.SaveChanges();
            _repository.Dispose();
        }
    }
}