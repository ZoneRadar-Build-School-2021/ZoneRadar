using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class ShopCarViewModel
    {
        public string SpaceName { get; set; }
        public string SpaceUrl { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Money { get; set; }
        public int OrderID { get; set; }
        public List<RentDetailViewModel> RentDetailVM { get; set; }
    }

    public class RentDetailViewModel
    {
        public string RentTime { get; set; }
        public string RentBackTime { get; set; }
        public int People { get; set; }
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
    }
}