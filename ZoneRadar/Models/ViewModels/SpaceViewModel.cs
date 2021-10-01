using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Models.ViewModels
{
    public class SpaceViewModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int District { get; set; }
        public string DistrictName { get; set; }
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public int CancellationId { get; set; }
        public string CancellationTile { get; set; }
        public string CancellationDetail { get; set; }
        public int ParkingId { get; set; }
        public string ParkingTitle { get; set; }

        /// <summary>
        /// 資料庫有的表
        /// </summary>
        public List<AddressArae> addressAraeList { get; set; }
        public List<AmenityArae> amenityAraeList { get; set; }
        public List<CancellationArae> cancellationAraesList { get; set; }
        public List<>

        /// <summary>
        /// 資料庫沒有的表
        /// </summary>
        public List<SelectListItem> Spacetype { get; set; }
        public List<SelectListItem> Parking { get; set; }
        public List<SelectListItem> CleanFisrtPart { get; set; }
        public List<SelectListItem> CleanSecPart { get; set; }
        public List<SelectListItem> CleanThirdPart { get; set; }
        public List<SelectListItem> CleanFourthPart { get; set; }
        public List<SelectListItem> Operating { get; set; }
        
        

    }
    /// <summary>
    /// 場地地址區要的資料
    /// </summary>
    public class AddressArae 
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int District { get; set; }
        public string DistrictName { get; set; }
    }
    /// <summary>
    ///場地類型區要的資料
    /// </summary>
    public class SpaceTypeArae
    {
        public int 

    }
    /// <summary>
    /// 便利設施區要的資料
    /// </summary>
    public class AmenityArae
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }

    }
    /// <summary>
    /// 取消區要的資料
    /// </summary>
    public class CancellationArae
    {
        public int CancellationId { get; set; }
        public string CancellationTitle { get; set; }
        public string CancellationDetail { get; set; }

    }
    
    //public class OperatingDetailArae
    //{
    //    public TimeSpan Time { get; set; }
    //}
}