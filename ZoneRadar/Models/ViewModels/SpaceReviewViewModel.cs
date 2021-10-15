using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class SpaceReviewViewModel
    {
        public int Score { get; set; }
        public string ReviewContent { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool IsRecommend { get; set; }
        public string ReviewedMemberName { get; set; }
        public string ReviewedMemberPhoto { get; set; }
        public int UserID { get; set; }
    }
}