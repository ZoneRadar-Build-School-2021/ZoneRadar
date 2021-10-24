using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class PreOrderViewModel
    {
        public int SpaceID { get; set; }
        public List<string> DatesArr { get; set; }
        public List<string> AttendeesArr { get; set; }
        public List<string> StartTimeArr { get; set; }
        public List<string> EndTimeArr { get; set; }
        public int HoursForDiscount { get; set; }
        public decimal Discount { get; set; }
    }
}