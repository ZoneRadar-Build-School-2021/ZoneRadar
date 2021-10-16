using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class UserInfoViewModel
    {
        public User User { get; set; }
        public List<UserReview> ToUserReview { set; get; 
    }
}
    public class UserReview : Spaces
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string ReviewContent { get; set; }
        public bool Recommend { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}