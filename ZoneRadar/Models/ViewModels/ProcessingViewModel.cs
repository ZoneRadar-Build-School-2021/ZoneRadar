using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class ProcessingViewModel
    {
        public int OrderId { get; set; }
        public string SpaceName { get; set; }
        public string OrderName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public List<OrderDetailesforPrcess> orderdetailesforprcess { get; set; }

    }
    public class OrderDetailesforPrcess 
    {
        public DateTime StratTime { get; set; }
        public DateTime EndTime { get; set; }
        public int People { get; set; }
    }
}