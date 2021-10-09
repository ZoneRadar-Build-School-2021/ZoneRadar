using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Models.ViewModels
{
    public class FilterViewModel
    {
        public Dictionary<string, List<string>> CityDistrictDictionary { get; set; }
        public List<string> SpaceTypeList { get; set; }
        public List<string> AmenityList { get; set; }
        public List<string> AmenityIconList { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedType { get; set; }
        public string SelectedDate { get; set; }
    }
}