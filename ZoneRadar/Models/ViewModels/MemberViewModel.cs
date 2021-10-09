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

    /// <summary>
    /// 註冊欄位的ViewModel
    /// </summary>
    public class RegisterZONERadarViewModel
    {
        [Required(ErrorMessage = "請填寫此欄位")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email輸入格式錯誤")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "最少需6個字元")]
        public string Password { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "最少需6個字元")]
        [Compare("Password", ErrorMessage = "密碼不一致！")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// 登入欄位的ViewModel
    /// </summary>
    public class LoginZONERadarViewModel
    {
        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email輸入格式錯誤")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "最少需6個字元")]
        public string Password { get; set; }
    }

    /// <summary>
    /// 註冊結果(含會員資料)
    /// </summary>
    public class RegisterResult
    {
        public Member User { get; set; }
        public bool IsSuccessful { get; set; }
    }

    /// <summary>
    /// 登入後會記錄在表單驗證票證中的會員資料
    /// </summary>
    public class UserInfo
    {
        public int MemberId { get; set; }
        public string MemberPhoto { get; set; }
    }
}