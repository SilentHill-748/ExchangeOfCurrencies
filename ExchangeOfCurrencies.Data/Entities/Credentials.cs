namespace ExchangeOfCurrencies.Data.Entities
{
    public class Credentials
    {
        public Credentials()
        {
            Login = string.Empty;
            Password = string.Empty;
        }


        public int CredentialsId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
