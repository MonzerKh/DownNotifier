using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ModelsLayer.ValidateAttribute
{
    public class UrlAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*[^\.\,\)\(\s]$";
            Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (!regex.IsMatch((string)value))
            {
                return new ValidationResult("Invalid URL.");
            }

            return ValidationResult.Success;
        }
    }
}
