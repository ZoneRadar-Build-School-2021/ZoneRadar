using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class SearchingPageViewModel
    {
        /// <summary>
        /// 場地編號
        /// </summary>
        public int SpaceID { get; set; }
        /// <summary>
        /// 場地價格
        /// </summary>
        public decimal PricePerHour { get; set; }
        /// <summary>
        /// 場地照片(Cloudinary)
        /// </summary>
        public List<string> SpaceImageURLList { get; set; }
        /// <summary>
        /// 場地名稱
        /// </summary>
        public string SpaceName { get; set; }
        /// <summary>
        /// 場地地址(國家)
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 場地地址(城市)
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 場地地址(區)
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 場地地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 場地容納人數
        /// </summary>
        public int Capacity { get; set; }
        /// <summary>
        /// 場地最低預定時數
        /// </summary>
        public int MinHour { get; set; }
        /// <summary>
        /// 場地面積
        /// </summary>
        public decimal MeasurementOfArea { get; set; }
        /// <summary>
        /// 場地評分
        /// </summary>
        public List<int> Scores { get; set; }
    }
}