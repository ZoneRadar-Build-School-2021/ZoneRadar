using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class UsercenterCompletedViewModel
    {
        public int OrderNumber { get; set; }
        public string OrederStatus { get; set; }
        public DateTime PublishTime { get; set; }
        public string SpaceName { get; set; }
        public string SpaceUrl { get; set; }
        public DateTime RentTime { get; set; }
        public DateTime RentBackTime { get; set; }
        public int People { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Money { get; set; }
        public bool HasReview { get; set; }
        public int OrderId { get; set; }
        public int Score { get; set; }
        public string ReviewContent { get; set; }
        public bool Recommend { get; set; }
        public int SpaceID { get; set; }
    }
}