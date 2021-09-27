using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;

namespace ZoneRadar.Models.ViewModel
{
    public class FormAreaViewModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int DistrictId { get; set; }
        public string DistricName { get; set; }

        public List<adreessCard> adreessList { get; set; }
        public List<AmenityCard> AmenityList { get; set; }
    }
    public class AmenityCard
    {
        public int AmenityDetailID { get; set; }
        public string Amenity { get; set; }

    }
    public class adreessCard
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int DistrictId { get; set; }
        public string DistricName { get; set; }

    }
}