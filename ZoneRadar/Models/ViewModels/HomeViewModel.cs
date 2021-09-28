using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Models.ViewModels
{
    /// <summary>
    /// 所有頁面的ViewModel
    /// </summary>
    public class AllViewModel
    {
        public HomeViewModel HomeVM { get; set; }
        public HomepageSearchViewModel HomepageSearchVM { get; set; }
        public RegisterZONERadarViewModel RegisterZONERadarVM { get; set; }
    }
    /// <summary>
    /// 首頁的ViewModel
    /// </summary>
    public class HomeViewModel
    {
        public List<SelectedSpaceViewModel> SelectedSpaces { get; set; }
        public List<ToSpaceReviewViewModel> ToSpaceReviews { get; set; }
        public List<SelectListItem> TyoeOptions { get; set; }
        public List<SelectListItem> CityOptions { get; set; }
        public string MemberPhoto { get; set; }
    }
    /// <summary>
    /// 精選場地
    /// </summary>
    public class SelectedSpaceViewModel
    {
        public int SpaceId { get; set; }
        public string CityName { get; set; }
        public int Capacity { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal PricePerHour { get; set; }
        public string SpacePhoto { get; set; }
        public double Score { get; set; }
    }
    /// <summary>
    /// 用戶給場地的評論
    /// </summary>
    public class ToSpaceReviewViewModel
    {
        public int SpaceId { get; set; }
        public string MemberName { get; set; }
        public string ReviewContent { get; set; }
        public int Score { get; set; }
    }
    /// <summary>
    /// 首頁搜尋列的ViewModel
    /// </summary>
    public class HomepageSearchViewModel
    {
        public string Type { get; set; }
        public int CityId { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }
    }
}