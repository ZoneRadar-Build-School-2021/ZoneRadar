using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class AddSpaceViewModel
    {
        public int SelectOptionCountryID { get; set; }
        public int SelectOptionCityID { get; set; }
        public int DistrictID { get; set; }
        public string Address { get; set; }
    }
}