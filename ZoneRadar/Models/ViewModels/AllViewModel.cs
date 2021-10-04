using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class AllViewModel
    {
        /// <summary>
        /// 所有頁面的ViewModel
        /// </summary>
        public HomeViewModel HomeVM { get; set; }
        public HomepageSearchViewModel HomepageSearchVM { get; set; }
        public RegisterZONERadarViewModel RegisterZONERadarVM { get; set; }
        public LoginZONERadarViewModel LoginZONERadarVM { get; set; }
    }
}