using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
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