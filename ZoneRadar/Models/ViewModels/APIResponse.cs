using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class APIResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
    }
}