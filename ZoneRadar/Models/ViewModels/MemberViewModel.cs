using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZoneRadar.Utilities;

namespace ZoneRadar.Models.ViewModels
{
    public class MemberViewModel
    {

    }

    /// <summary>
    /// 註冊欄位的ViewModel(Jenny)
    /// </summary>
    public class RegisterZONERadarViewModel
    {
        [Required(ErrorMessage = "請填寫此欄位")]
        public string Name { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email輸入格式錯誤")]
        public string RegisterEmail { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "密碼長度需為6~50字元")]
        [QualifiedPassword(ErrorMessage = "密碼必須包含至少1個數字、小寫英文和大寫英文")]
        public string RegisterPassword { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "密碼長度需為6~50字元")]
        [Compare("RegisterPassword", ErrorMessage = "密碼不一致")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// 登入欄位的ViewModel(Jenny)
    /// </summary>
    public class LoginZONERadarViewModel
    {
        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email輸入格式錯誤")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "密碼長度需為6~50字元")]
        public string LoginPassword { get; set; }
    }

    /// <summary>
    /// 註冊/登入結果(含會員資料)(Jenny)
    /// </summary>
    public class MemberResult
    {
        public Member User { get; set; }
        public bool IsSuccessful { get; set; }
        public string ShowMessage { get; set; }
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// 登入後會記錄在表單驗證票證中的會員資料(Jenny)
    /// </summary>
    public class UserInfo
    {
        public int MemberId { get; set; }
        public string MemberPhoto { get; set; }
    }

    /// <summary>
    /// 重設密碼的ViewModel(Jenny)
    /// </summary>
    public class ResetZONERadarPasswordViewModel
    {
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "密碼長度需為6~50字元")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "密碼長度需為6~50字元")]
        [Compare("NewPassword", ErrorMessage = "密碼不一致")]
        public string NewConfirmPassword { get; set; }
    }

    /// <summary>
    /// JS登入結果(含會員資料)(Jenny)
    /// </summary>
    public class JSMemberResult
    {
        public string Photo { get; set; }
        public bool IsSuccessful { get; set; }
        public string ShowMessage { get; set; }
        public string ExceptionMsg { get; set; }
    }

    /// <summary>
    /// 以Google帳號註冊會員(Jenny)
    /// </summary>
    public class RegisterWithGoogle
    {
        public string GoogleId { get; set; }
        public string GoogleEmail { get; set; }
        public string GoogleName { get; set; }
        public string GooglePhoto { get; set; }
    }
}