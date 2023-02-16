using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ModelsLayer.ValidateAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CustomEmailAddressAttribute : ValidationAttribute
    {
        private readonly Regex _regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

        public CustomEmailAddressAttribute() : base("The {0} field is not a valid email address.") { }

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            return _regex.IsMatch(value.ToString());
        }
    }


}
