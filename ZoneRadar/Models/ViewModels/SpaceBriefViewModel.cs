using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class SpaceBriefViewModel
    {
        public int SpaceID { get; set; }
        public decimal PricePerHour { get; set; }
        public List<string> SpaceImageURLList { get; set; }
        public string SpaceName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public int MinHour { get; set; }
        public decimal MeasurementOfArea { get; set; }
    }
}