using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinNavigation.Validations
{
    public class EmailRule : IValidationRule<string>
    {
        public EmailRule()
        {
            ValidationMessage = "Should be an email address";
        }

        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return true;
            // todo
            //return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(value);
        }
    }
}
