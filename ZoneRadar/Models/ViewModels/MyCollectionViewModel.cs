using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class MyCollectionViewModel
    {
        public User User { set; get; }   
        public List<Spaces> MyCollection { get; set;}
    }
    
}