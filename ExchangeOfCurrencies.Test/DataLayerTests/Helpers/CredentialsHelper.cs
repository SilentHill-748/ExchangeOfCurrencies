using ExchangeOfCurrencies.Data.Entities;

namespace ExchangeOfCurrencies.Test.DataLayerTests.Helpers
{
    internal class CredentialsHelper
    {
        public static Credentials GetCredentials(string login, string password)
        {
            return new Credentials()
            {
                Login = login,
                Password = password
            };
        }
    }
}
