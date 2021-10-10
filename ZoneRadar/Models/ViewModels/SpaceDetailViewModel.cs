using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class SpaceDetailViewModel
    {
        public string HostName { get; set; }
        public string HostPhoto { get; set; }
        public Dictionary<string, List<string>> CleaningOptionDict { get; set; }
        public string Introduction { get; set; }
        public string ShootingEquipment { get; set; }
        public string ParkingInfo { get; set; }
        public string HostRule { get; set; }
        public Dictionary<string, List<string>> AmenityDict { get; set; }
        public Dictionary<string, List<string>> AmenityIconDict { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string TrafficInfo { get; set; }
        public List<string> OperatingDayList { get; set; }
        public List<TimeSpan> StartTimeList { get; set; }
        public List<TimeSpan> EndTimeList { get; set; }
        public string CancellationTitle { get; set; }
        public string CancellationInfo { get; set; }
        public int HoursForDiscount { get; set; }
        public Decimal Discount { get; set; }
    }
}