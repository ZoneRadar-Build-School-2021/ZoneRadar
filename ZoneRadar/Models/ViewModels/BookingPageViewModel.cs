using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class BookingPageViewModel
    {
        public List<SpaceReviewViewModel> Reviews { get; set; }
        public SpaceBriefViewModel SpaceBreifInfo { get; set; }
        public SpaceDetailViewModel SpaceDetailInfo { get; set; }
    }

    public class SpaceReviewViewModel
    {
        /// <summary>
        /// 場地評分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 場地評價內容
        /// </summary>
        public string ReviewContent { get; set; }
        /// <summary>
        /// 場地評價時間
        /// </summary>
        public DateTime ReviewDate { get; set; }
        /// <summary>
        /// 是否推薦
        /// </summary>
        public bool IsRecommend { get; set; }
        /// <summary>
        /// 評價人名單
        /// </summary>
        public string ReviewedMemberName { get; set; }
        /// <summary>
        /// 評價人照片
        /// </summary>
        public string ReviewedMemberPhoto { get; set; }
    }

    public class SpaceBriefViewModel
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
    }

    public class SpaceDetailViewModel
    {
        /// <summary>
        /// 場地主名稱
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// 場地主照片
        /// </summary>
        public string HostPhoto { get; set; }
        /// <summary>
        /// 清潔公約
        /// </summary>
        public Dictionary<string, List<string>> CleaningOptionDict { get; set; }
        /// <summary>
        /// 場地簡介
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// 攝影及錄影設備
        /// </summary>
        public string ShootingEquipment { get; set; }
        /// <summary>
        /// 停車資訊
        /// </summary>
        public string ParkingInfo { get; set; }
        /// <summary>
        /// 場地規範
        /// </summary>
        public string HostRule { get; set; }
        /// <summary>
        /// 便利設施
        /// </summary>
        public Dictionary<string, List<string>> AmenityDictionary { get; set; }
        /// <summary>
        /// 經度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 緯度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 交通資訊
        /// </summary>
        public string TrafficInfo { get; set; }
        /// <summary>
        /// 服務日期
        /// </summary>
        public List<string> OperatingDayList { get; set; }
        /// <summary>
        /// 開始時間
        /// </summary>
        public List<TimeSpan> StartTimeList { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        public List<TimeSpan> EndTimeList { get; set; }
        /// <summary>
        /// 取消政策標題
        /// </summary>
        public string CancellationTitle { get; set; }
        /// <summary>
        /// 取消政策內容
        /// </summary>
        public string CancellationInfo { get; set; }
        /// <summary>
        /// 滿時優惠
        /// </summary>
        public int HoursForDiscount { get; set; }
        /// <summary>
        /// 滿時優惠
        /// </summary>
        public Decimal Discount { get; set; }
    }
}