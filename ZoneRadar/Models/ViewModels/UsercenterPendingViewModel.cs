using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class UsercenterPendingViewModel
    {
        public int OrderNumber { get; set; }
        public DateTime PaidTime { get; set; }
        public string SpaceName { get; set; }
        public string SpaceUrl { get; set; }
        public int Score { get; set; }
        public DateTime RentTime { get; set; }
        public DateTime RentBackTime { get; set; }
        public int People { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Money { get; set; }
        public string CancelTitle { get; set; }
        public string CancelDetail { get; set; }
        public string CancelTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal CancelMoney { get; set; }

        public int OrderId { get; set; }
        public int OrderStatus { get; set; }
        public int MemberId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public int SpaceID { get; set; }
    }
}