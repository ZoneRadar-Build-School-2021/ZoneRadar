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
}