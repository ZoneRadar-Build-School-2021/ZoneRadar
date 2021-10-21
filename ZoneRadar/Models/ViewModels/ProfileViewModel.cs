using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    /// <summary>
    /// 會員個人資訊VM(昶安)
    /// </summary>
    public class ProfileViewModel
    {
        public int MemberID { get; set; }
        public string Photo { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool ReceiveEDM { get; set; }
    }
}