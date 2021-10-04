using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class SomeOnesSpaceViewModel
    {
        public List<SomeOnesCountry> SomeOnesCountryList { get; set; }

        public List<SomeOnesSpace> SomeOnesSpaceList { get; set; }
        public List<SomeOnesDistrict> SomeOnesDistrictList { get; set; }
        public List<SomeOnesCity> SomeOnesCitytList { get; set; }
        public List<SomeOnesSpaceType>SomeOnesSpaceTypeList { get; set; }
        public List<SomeOnesTypeDetail> SomeOnesTypeDetailList { get; set; }
        public List<ShowAllTypeDetail> ShowAllTypeDetailList { get; set; }
    }
    public class SomeOnesSpace
    {
        public int SpaceID { get; set; }

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

        public int CountryID { get; set; }

        public int CityID { get; set; }

        public int DistrictID { get; set; }

        public string Address { get; set; }

        public DateTime PublishTime { get; set; }

        public bool Discontinued { get; set; }
        public virtual Cancellation Cancellation { get; set; }

        public virtual Country Country { get; set; }

        public virtual District District { get; set; }

        public virtual Member Member { get; set; }


    }
    public class SomeOnesCountry
    {
        public string CountryName { get; set; }
    }
    public class SomeOnesDistrict 
    {
        public string DistrictName { get; set; }
    }
    public class SomeOnesCity 
    {
        public string CityName { get; set; }
    }
    public class SomeOnesSpaceType 
    {
        public int SpaceTypeId { get; set; }
        public int TypeDetailID { get; set; }
    }
    public class ShowAllTypeDetail
    {
        public int TypeDetailId { get; set; }
        public string Type { get; set; }
    }
    public class SomeOnesTypeDetail 
    {
        public int TypeDetailId { get; set; }
        public string Type { get; set; }
    }
}