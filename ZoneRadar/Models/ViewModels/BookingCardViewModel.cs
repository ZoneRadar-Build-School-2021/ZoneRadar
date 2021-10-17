using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class BookingCardViewModel
    {
        public List<int> OperatingDayList { get; set; }
        public List<string> StartTimeList { get; set; }
        public List<string> EndTimeList { get; set; }
        public int HoursForDiscount { get; set; }
        public Decimal Discount { get; set; }
        public int MinHour { get; set; }
        public int PricePerHour { get; set; }
        public List<string> OrderTimeList { get; set; }
    }
}