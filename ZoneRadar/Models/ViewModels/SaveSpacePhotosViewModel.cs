using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class SaveSpacePhotosViewModel
    {
        public int SpaceID { get; set; }
        public List<string> PhotoUrlList { get; set; }
    }
}