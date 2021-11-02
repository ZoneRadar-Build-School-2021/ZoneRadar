using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class PaymentViewModel : OrderViewModel
    {
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public decimal PricePerHour { get; set; }
        public string CancellationTitle { get; set; }
        public string CancellationDetail { get; set; }
        public decimal Discounthours { get; set; }
        public decimal DiscountPrice { get; set; } 
    }
}