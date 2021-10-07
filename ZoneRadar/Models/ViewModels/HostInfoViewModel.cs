using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class HostInfoViewModel
    {
        public User User { set; get; }
        public List<Spaces> Spaces { get; set; }
    }
    public class User 
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string Photo { get; set; }
       
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }
        public DateTime SignUpDateTime { get; set; }
    }
    public class Spaces 
    {
        public string SpaceName { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public decimal PricePerHour { get; set; }
        public string SpacePhoto { get; set; }
        public int ReviewCount { get; set; }
    }
}