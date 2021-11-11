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
        public const int NUM_OF_PRELOADED_BRIEF_REVIEW = 3;
        public const int NUM_OF_PRELOADED_REVIEW = 2;
    }
}