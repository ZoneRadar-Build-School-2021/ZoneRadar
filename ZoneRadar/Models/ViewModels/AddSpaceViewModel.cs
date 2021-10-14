using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class AddSpaceViewModel
    {
        public Guid[] SelectOptionCountryID { get; set; }
        public Guid[] SelectOptionCityID { get; set; }
        public int DistrictID { get; set; }
        public string Address { get; set; }
    }
}