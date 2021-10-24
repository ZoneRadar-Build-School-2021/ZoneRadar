using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    // 訂單 時間+人數 (OrderDetail)
    public class RentDetailViewModel
    {
        public string RentTime { get; set; }
        public string RentBackTime { get; set; }
        public int People { get; set; }
        public decimal Money { get; set; }
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
    }
    // 訂單
    public class OrderViewModel
    {
        public int SpaceId { get; set; }
        public int OrderNumber { get; set; }
        public string PaidTime { get; set; }
        public string SpaceName { get; set; }
        public string SpaceUrl { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public double Score { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal TotalMoney { get; set; }
        public string Email { get; set; }
        public int OrderId { get; set; }
        public List<RentDetailViewModel> RentDetail { get; set; }
    }
    // 預購單 CartVM
    public class CartsViewModel : OrderViewModel
    {

    }
    // 訂單 UsercenterPendingVM
    public class UsercenterPendingViewModel : OrderViewModel
    {
        public string CancelTitle { get; set; }
        public string CancelDetail { get; set; }
        public string CancelTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal CancelMoney { get; set; }
        public int OrderStatus { get; set; }
        public int MemberId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
    }
    // 訂單 UsercenterProcessingVM
    public class UsercenterProcessingViewModel : OrderViewModel
    {
        
    }
    // 訂單 UsercenterCompletedVM
    public class UsercenterCompletedViewModel : OrderViewModel
    {
        public string OrederStatus { get; set; }
        public bool HasReview { get; set; }
        public string ReviewContent { get; set; }
        public bool Recommend { get; set; }
    }
    // 訂單 HostcenterHistoryVM
    public class HostCenterHistoryViewModel : OrderViewModel
    {
        public string UserName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public bool HasReview { get; set; }
        public string ReviewContent { get; set; }
        public bool Recommend { get; set; }
    }
}