using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Utilities
{
    /// <summary>
    /// 指定資料欄位中必須包含至少1個數字、小寫英文和大寫英文
    /// </summary>
    public class QualifiedPasswordAttribute : ValidationAttribute, IClientValidatable
    {
        //When implemented in a class, returns client validation rules for that class. 前端的Model驗證
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ValidationType = "safe",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            yield return rule;
        }

        //Determines whether the specified value of the object is valid. 後端的Model驗證
        public override bool IsValid(object value)
        {
            string password = (string)value;
            Regex rgx = new Regex(@"^(?!.*[^\x21-\x7e])(?=.{6,50})(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$");

            return rgx.IsMatch(password);
        }
    }
}