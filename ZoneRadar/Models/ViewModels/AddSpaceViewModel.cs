using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class AddSpaceViewModel
    {
        public int SpaceID { get;set; }
        public int CountryID { get; set; }
        public string CityID { get; set; }
        public int DistrictID { get; set; }
        public string Address { get; set; }
        public int MemberID { get; set; }
        public string SpaceName { get; set; }
        public string Introduction { get; set; }
        public decimal MeasureOfArea { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public int MinHours { get; set; }
        public string HostRules { get; set; }
        public string Traffic { get; set; }
        public string Parking { get; set; }
        public string ShootingEquipment { get; set; }
        public int CancellationID { get; set; }
        public DateTime PublishTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int SpaceStatusID { get; set; }
        public DateTime DiscontinuedDate { get; set; }

        public int OperatingDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


        public int TypeDetailID { get; set; }


    }
}