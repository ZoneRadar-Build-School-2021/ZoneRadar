using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class MemberViewModel
    {

    }
    public class RegisterZONERadarViewModel
    {
        [Required(ErrorMessage = "此欄為必填")]
        public string Email { get; set; }

        [Required(ErrorMessage = "此欄為必填")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "最少需6個字元")]
        public string Password { get; set; }


        [Required(ErrorMessage = "此欄為必填")]
        [StringLength(20)]
        public string Name { get; set; }

    }
}