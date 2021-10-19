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
        public string PaidTime { get; set; }
        public string SpaceName { get; set; }
        public string SpaceUrl { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public double Score { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Money { get; set; }
        public int SpaceID { get; set; }
        public string Email { get; set; }
        public int OrderId { get; set; }
        public List<RentDetailViewModel> RentDetail { get; set; }
    }
}