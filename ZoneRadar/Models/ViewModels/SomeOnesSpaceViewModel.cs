using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Models.ViewModels
{
    public class SomeOnesSpaceViewModel
    {
        public List<SomeOnesCountry> SomeOnesCountryList { get; set; }

        public List<SomeOnesSpace> SomeOnesSpaceList { get; set; }
        public List<SomeOnesDistrict> SomeOnesDistrictList { get; set; }
        public List<SomeOnesCity> SomeOnesCitytList { get; set; }
        public List<SomeOnesSpaceType> SomeOnesSpaceTypeList { get; set; }
        public List<SomeOnesTypeDetail> SomeOnesTypeDetailList { get; set; }
        public List<ShowAllTypeDetail> ShowAllTypeDetailList { get; set; }
        public List<SomeOnesSpaceName> SomeOnesSpaceNameList { get; set; }
        public List<SomeOnesSpaceName> SpaceOwnerNameList { get; set; }

        public List<SomeOnesSpaceIntroduction> SomeOnesSpaceIntroductionList { get; set; }
        public List<SomeOnesMeasureOfAreaandCapacity> SomeOnesMeasureOfAreaandCapacityList { get; set; }
        public List<SomeOnesPrice> SomeOnesPriceList { get; set; }
        public List<SomeOnesDiscount> SomeOnesDiscountsList { get; set; }

        public List<SomeOnesAmenity> SomeOnesAmenityList { get; set; }
        public List<SomeOnesAmenity> SomeTwoAmenityList { get; set; }
        public List<SomeOnesAmenity> SomeThreeAmenityList { get; set; }

        public List<AmenityAraeOne> amenityAraeOneList { get; set; }
        public List<AmenityAraeTwo> amenityAraeTwoList { get; set; }
        public List<AmenityAraeThree> amenityAraeThreeList { get; set; }


        public List<SomeOnesRules> SomeOnesRulesList { get; set; }

        public List<SomeOnesTraffic> SomeOnesTrafficList { get; set; }
        public List<SomeOnesParking> SomeOnesParkingList { get; set; }
        public List<SomeOnesShooting> SomeOnesShootingList { get; set; }
        public List<SomeOnesCleanRule> CleanRuleOptionsOneList { get; set; }

        public List<SomeOnesCleanRule> CleanRuleOptionsTwoList { get; set; }
        public List<SomeOnesCleanRule> CleanRuleOptionsThreeList { get; set; }
        public List<SomeOnesCleanRule> CleanRuleOptionsFourList { get; set; }

        public List<SomeOnesCleanRule> SomeOnesCleanRuleOneList { get; set; }
        public List<SomeOnesCleanRule> SomeOnesCleanRuleTwoList { get; set; }
        public List<SomeOnesCleanRule> SomeOnesCleanRuleThreeList { get; set; }
        public List<SomeOnesCleanRule> SomeOnesCleanRuleFourList { get; set; }

        public List<SelectListItem> Operating { get; set; }
        public List<SelectListItem> OperatingDay { get; set; }

        public List<SomeOnesCancel> SomeOnesCancelAllList { get; set; }
        public List<SomeOnesCancel> SomeOnesCancelList { get; set; }
        public List<SpaceoperatingDay> SpaceoperatingDaysList { get; set; }
        public List<SpaceoperatingDay> _compareOperatingDay { get; set; }
    }

    public class SomeOnesSpaceName
    {
        public string SpaceName { get; set; }
        public string MamberName { get; set; }

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
        public bool check { get; set; }
    }
    public class SomeOnesSpaceIntroduction
    {
        public string Introduction { get; set; }
    }
    public class SomeOnesMeasureOfAreaandCapacity
    {
        public decimal MeasureOfArea { get; set; }
        public int Capacity { get; set; }
    }
    public class SomeOnesPrice
    {
        public decimal PricePerHour { get; set; }
        public int MinHours { get; set; }

    }
    public class SomeOnesDiscount
    {
        public int SpaceId { get; set; }
        public int Hours { get; set; }
        public decimal Discount { get; set; }

    }
    public class SomeOnesAmenity
    {
        public int AmenityId { get; set; }
        public int AmenityCategoryID { get; set; }
        public string Amenity { get; set; }
    }
    public class SomeOnesRules
    {
        public string Rules { get; set; }
    }
    public class SomeOnesTraffic
    {
        public string Traffic { get; set; }
    }
    public class SomeOnesParking
    {
        public string Parking { get; set; }
    }
    public class SomeOnesShooting
    {
        public string Shooting { get; set; }
    }
    public class SomeOnesCleanRule
    {
        public int CleaningCategoryID { get; set; }
        public int CleaningOptionID { get; set; }
        public string OptionDetail { get; set; }
    }
    public class SomeOnesCancel
    {
        public int CancellationID { get; set; }
        public string CancellationTitle { get; set; }
        public string CancellationDetail { get; set; }
    }
    public class SpaceoperatingDay
    {
        public int SpaceId { get; set; }
        public int OperatingDay { get; set; }
        public string weekDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

}