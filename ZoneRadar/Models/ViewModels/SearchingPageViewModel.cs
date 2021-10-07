using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class SearchingPageViewModel
    {
        public SpaceBriefViewModel BriefInfo { get; set; }
        public List<int> Scores { get; set; }
    }
}