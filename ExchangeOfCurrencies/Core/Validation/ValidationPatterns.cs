namespace ExchangeOfCurrencies.Core.Validation
{
    public class ValidationPatterns
    {
        public const string NamePattern = @"^([A-ZА-Я]{1})([а-яa-z]*)([^\d]$)";
        public const string PhonePattern = @"^((8|\+7)[\- ]?)(\d{3}?[\- ]?)?[\d\- ]{7,10}$";
        public const string EmailPattern = @"^([-0-9A-Za-z_\.])+@[-0-9a-z_^\.]+\.[a-z]{2,6}$";
        public const string LoginPattern = @"^([^0-9])([A-Za-z0-9]+)";
    }
}
