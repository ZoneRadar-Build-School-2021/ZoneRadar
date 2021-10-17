using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    /// <summary>
    /// 管理我的場地的ViewModel(Jenny)
    /// </summary>
    public class SpaceManageViewModel
    {
        public int SpaceID { get; set; }
        public string SpacePhotoUrl { get; set; }
        public string SpaceName { get; set; }
        public string SpaceAddress { get; set; }
        public double Score { get; set; }
        public int NumberOfReviews { get; set; }
        public int NumberOfOrders { get; set; }
        public int SpaceStatusId { get; set; }
        public string LastOrderdDate { get; set; }
        public string DiscontinuedDate { get; set; }
    }
}