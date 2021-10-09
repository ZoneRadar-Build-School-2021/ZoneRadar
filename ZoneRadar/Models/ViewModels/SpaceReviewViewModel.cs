using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
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
}