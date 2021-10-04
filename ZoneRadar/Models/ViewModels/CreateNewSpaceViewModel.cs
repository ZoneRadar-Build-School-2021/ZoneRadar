using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class CreateNewSpaceViewModel
    {
        public string SpaceName { get; set; }
        public string Introduction { get; set; }
        public decimal MeasureOfArea { get; set; }
        public int Capacity { get; set; }
        public int PricePerHour { get; set; }
    }
}