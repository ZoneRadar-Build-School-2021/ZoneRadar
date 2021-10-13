using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Models.ViewModels
{
    /// <summary>
    /// 首頁的ViewModel(Jenny)
    /// </summary>
    public class HomeViewModel
    {
        public List<SelectedSpaceViewModel> SelectedSpaces { get; set; }
        public List<ToSpaceReviewViewModel> ToSpaceReviews { get; set; }
        public List<SelectListItem> TyoeOptions { get; set; }
        public List<SelectListItem> CityOptions { get; set; }
        public QueryViewModel QueryVM { get; set; }
    }
    /// <summary>
    /// 精選場地(Jenny)
    /// </summary>
    public class SelectedSpaceViewModel
    {
        public int SpaceId { get; set; }
        public string CityName { get; set; }
        public int Capacity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal PricePerHour { get; set; }
        public string SpacePhoto { get; set; }
        public double Score { get; set; }
    }
    /// <summary>
    /// 用戶給場地的評論(Jenny)
    /// </summary>
    public class ToSpaceReviewViewModel
    {
        public int SpaceId { get; set; }
        public string MemberName { get; set; }
        public string ReviewContent { get; set; }
        public int Score { get; set; }
    }
}