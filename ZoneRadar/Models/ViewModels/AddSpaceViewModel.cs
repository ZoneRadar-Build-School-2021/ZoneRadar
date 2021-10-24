using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class AddSpaceViewModel
    {
        public int SpaceID { get;set; }
       
        [Required]
        public int CountryID { get; set; }
       
        [Required(ErrorMessage = "請選擇縣市")]
        public string CityID { get; set; }

        [Required]
        public int DistrictID { get; set; }
        
        [Required]
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
        public string Lat { get; set; }
        public string Lng{ get; set; }
        public int SpaceStatusID { get; set; }
        public DateTime DiscontinuedDate { get; set; }


        public List<int> OperatingDay { get; set; }
        public string Hours1 { get; set; }
        public string Hours2 { get; set; }
        public string Hours3 { get; set; }
        public string Hours4 { get; set; }
        public string Hours5 { get; set; }
        public string Hours6 { get; set; }
        public string Hours7 { get; set; }
        public List<string> StartTime { get; set; }
        public List<string> EndTime { get; set; }

        public List<string> SpacePhotoUrl { get; set; }

        public List<int> TypeDetailID { get; set; }

        public List<int> AmenityDetailID { get; set; }

        public List<int> CleaningOptionID { get; set; }

        public int Hour { get; set; }
        public decimal Discount { get; set; }


    }
}