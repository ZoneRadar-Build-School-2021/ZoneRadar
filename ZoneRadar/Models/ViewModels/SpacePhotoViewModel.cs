using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class SpacePhotoViewModel
    {
        public string Name { get; set; }
        public string Preset { get; set; }
        public List<string> PhotoUrlList { get; set; }
    }
}