using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ZoneRadar.Utilities
{
    public class QualifiedPasswordAttribute : ValidationAttribute, IClientValidatable
    {
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ValidationType = "safe",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            yield return rule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = (string)value;
            Regex rgx = new Regex(@"^(?!.*[^\x21-\x7e])(?=.{6,50})(?=.*[a-zA-Z])(?=.*\d).*$", RegexOptions.IgnoreCase);

            if (rgx.IsMatch(password))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("密碼必須包含至少1個數字、小寫英文和大寫英文");
            }
        }
        //public override bool IsValid(object value)
        //{
        //    string password = (string)value;
        //    Regex rgx = new Regex(@"^(?!.*[^\x21-\x7e])(?=.{6,50})(?=.*[a-zA-Z])(?=.*\d).*$", RegexOptions.IgnoreCase);

        //    if (rgx.IsMatch(password))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}