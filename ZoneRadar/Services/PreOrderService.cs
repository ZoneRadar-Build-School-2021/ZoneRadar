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
        public List<OrderViewModel> GetShopCarVM(int userid)
        {
            var result = new List<OrderViewModel>();
            //訂單 ( 該會員ID 且 訂單狀態是預購單 且 場地狀態是上架中 )
            var orders = _repository.GetAll<Order>().Where(x => x.MemberID == userid && x.OrderStatusID == 1 && x.Space.SpaceStatusID == 2);
            var reviews = _repository.GetAll<Review>();
            foreach(var order in orders)
            {
                var resultDetail = new List<RentDetailViewModel>();
                foreach (var orderdetail in order.OrderDetail)
                {
                    resultDetail.Add(new RentDetailViewModel
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
                    SpaceName = order.Space.SpaceName,
                    SpaceUrl = order.Space.SpacePhoto.First().SpacePhotoUrl,
                    OwnerName = order.Space.Member.Name,
                    OwnerPhone = order.Space.Member.Phone,
                    //評分 = 訂單到評分表 找到 場地ID = 訂單場地ID 且 Tohost是True的
                    Score = reviews.Where(x => x.Order.SpaceID == order.SpaceID && x.ToHost).Select(x => x.Score).Average(),
                    TotalMoney = resultDetail.Select(x => x.Money).Sum(),
                    Email = order.Member.Email,
                    OrderId = order.OrderID,
                    RentDetail = resultDetail
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

        /// <summary>
        /// 及時算錢
        /// </summary>
        /// <param name="preOrderVM"></param>
        /// <returns></returns>
        public CalculateViewModel CalculatePrice(PreOrderViewModel preOrderVM)
        {
            var bookingDateList = preOrderVM.DatesArr;
            var startTimeList = preOrderVM.StartTimeArr;
            var endTimeList = preOrderVM.EndTimeArr;
            var hoursForDiscount = preOrderVM.HoursForDiscount;
            var discount = preOrderVM.Discount;
            var pricePerHour = preOrderVM.PricePerHour;

            var timeDiffList = new List<double>();
            for (int i = 0; i < bookingDateList.Count; i++)
            {
                var stratDateTime = DateTime.Parse($"{bookingDateList[i]}T{startTimeList[i]:00}");
                var endDateTime = DateTime.Parse($"{bookingDateList[i]}T{endTimeList[i]:00}");
                var timeDiff = (endDateTime.Subtract(stratDateTime).TotalMinutes);
                timeDiffList.Add((timeDiff));
            }

            var totalHour = (decimal)(timeDiffList.Sum() / 60);
            var totalPrice = totalHour * pricePerHour;
            if (totalHour >= hoursForDiscount)
            {
                totalPrice = totalPrice * discount;
            }

            var result = new CalculateViewModel
            {
                TotalHour = totalHour,
                TotalPrice = decimal.Round(totalPrice)
            };

            return result;
        }
    }
}