using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        /// <summary>
        /// 資料庫有的表
        /// </summary>
        public List<AddressArae> addressAraeList { get; set; }
        public List<AmenityAraeOne> amenityAraeOneList { get; set; }
        public List<AmenityAraeTwo> amenityAraeTwoList { get; set; }
        public List<AmenityAraeThree> amenityAraeThreeList { get; set; }
        public List<CancellationArae> cancellationAraesList { get; set; }
        public List<SpaceTypeArae> SpaceTypeAraeList { get; set; }
        public List<CleanFisrtPart> CleanFisrtPartList { get; set; }
        public List<CleanSecPart> CleanSecPartList { get; set; }
        public List<CleanThirdPart> CleanThirdPartList { get; set; }
        public List<CleanFourdPart> CleanFourdPartList { get; set; }



        /// <summary>
        /// 資料庫沒有的表
        /// </summary>
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
        public int TypeDetailId { get; set; }
        public string Type { get; set; }

    }
    /// <summary>
    /// 便利設施區要的資料
    /// </summary>
    public class AmenityAraeOne
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public int AmenityCategoryDetailId { get; set; }
    }
    /// <summary>
    /// 場地空間
    /// </summary>
    public class AmenityAraeTwo
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public int AmenityCategoryDetailId { get; set; }
    }
    /// <summary>
    /// 其他
    /// </summary>
    public class AmenityAraeThree
    {
        public int AmenityId { get; set; }
        public string AmenityName { get; set; }
        public int AmenityCategoryDetailId { get; set; }
    }
    /// <summary>
    /// 清潔條款的資料
    /// 分四類  1.CleaningPolicy
    /// </summary>
    public class CleanFisrtPart
    {
        public int CleaningCategoryId { get; set; }
        public int CleaningOptionId { get; set; }
        public string OptionDetail { get; set; }
    }
    /// <summary>
    /// 清潔條款的資料
    /// 2.ProtectiveGearDetail
    /// </summary>
    public class CleanSecPart
    {
        public int CleaningCategoryId { get; set; }
        public int CleaningOptionId { get; set; }
        public string OptionDetail { get; set; }
    }
    /// <summary>
    /// 清潔條款的資料
    /// 3.PhysicalDistanceDetail
    /// </summary>
    public class CleanThirdPart
    {
        public int CleaningCategoryId { get; set; }
        public int CleaningOptionId { get; set; }
        public string OptionDetail { get; set; }
    }
    /// <summary>
    /// 清潔條款的資料
    /// 4.SignageDetail
    /// </summary>
    public class CleanFourdPart
    {
        public int CleaningCategoryId { get; set; }
        public int CleaningOptionId { get; set; }
        public string OptionDetail { get; set; }
    }
    /// <summary>
    /// 取消區的資料
    /// </summary>
    public class CancellationArae
    {
        public int CancellationId { get; set; }
        public string CancellationTitle { get; set; }
        public string CancellationDetail { get; set; }

    }

}}