using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Models.ViewModels
{
    public class FilterViewModel
    {
        public List<string> CityList { get; set; }
        public List<SelectListItem> DistrictList { get; set; }
        public List<SelectListItem> SpaceTypeList { get; set; }
        public List<string> AmenityList { get; set; }
    }
}