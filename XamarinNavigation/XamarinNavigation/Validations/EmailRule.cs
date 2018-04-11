using System.ComponentModel.DataAnnotations;

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
            //return new EmailAddressAttribute().IsValid(value);
        }
    }
}
