using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{    
    public class SpaceStatusInformation
    {
        public int UserId { get; set; }
        public int SpaceId { get; set; }
        public int SpaceStatusId { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
    }
    public class SweetAlert
    {
        public bool Alert { get; set; }
        public string Message { get; set; }
        public bool Icon { get; set; }
        public string IconString { get; set; }
    }
}