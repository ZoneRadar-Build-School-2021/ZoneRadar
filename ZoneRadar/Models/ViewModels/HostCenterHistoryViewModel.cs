using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class HostCenterHistoryViewModel
    {
        public string SpaceName { get; set; }
        public string SpaceUrl { get; set; }
        public string UserName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Money { get; set; }
        public int OrderID { get; set; }
        public int SpaceID { get; set; }
        public int OrderNumber { get; set; }
        public string Email { get; set; }
        public bool HasReview { get; set; }
        public double Score { get; set; }
        public string ReviewContent { get; set; }
        public bool Recommend { get; set; }

        public List<RentDetailViewModel> RentDetailVM { get; set; }

    }

}