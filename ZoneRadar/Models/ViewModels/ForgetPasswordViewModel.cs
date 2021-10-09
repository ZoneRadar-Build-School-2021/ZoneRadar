using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZoneRadar.Models.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "請填寫此欄位")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email輸入格式錯誤")]
        public string Email { get; set; }
    }
}