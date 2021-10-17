using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class UsercenterProcessingViewModel
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
        public int SpaceID { get; set; }
        public string Email { get; set; }
    }
}