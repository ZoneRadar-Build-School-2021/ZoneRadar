using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class FilterViewModel
    {
        public List<string> CityList { get; set; }
        public List<string> DistrictList { get; set; }
        public List<string> SpaceTypeList { get; set; }
        public List<string> AmenityList { get; set; }
    }
}