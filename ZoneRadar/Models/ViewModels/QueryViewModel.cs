using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class QueryViewModel
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public string LowPrice { get; set; }
        public string HighPrice { get; set; }
        public string Attendees { get; set; }
        public string Area { get; set; }
        public List<string> Amenities { get; set; }
        public string Keywords { get; set; }
        public int SentCount { get; set; }
        public int AddCount { get; set; }
    }
}