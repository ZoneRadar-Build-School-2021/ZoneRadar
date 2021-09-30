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
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "此欄為必填")]
        public string Email { get; set; }

        [Required(ErrorMessage = "此欄為必填")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "最少需6個字元")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "最少需6個字元")]
        [Compare("Password", ErrorMessage = "密碼不一致")]
        public string ConfirmPassword { get; set; }
    }
    public class LoginZONERadarViewModel
    {
        [Required(ErrorMessage = "此欄為必填")]
        public string Email { get; set; }

        [Required(ErrorMessage = "此欄為必填")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "最少需6個字元")]
        public string Password { get; set; }
    }

    /// <summary>
    /// 註冊的狀態(含會員資料)
    /// </summary>
    public class RegisterStatus
    {
        public Member user { get; set; }
        public bool IsSuccessful { get; set; }
    }

    /// <summary>
    /// 登入後，會員的資料
    /// </summary>
    public class UserInfo
    {
        public int MemberId { get; set; }
        public string MemberPhoto { get; set; }
    }
}