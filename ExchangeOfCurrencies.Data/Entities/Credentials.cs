namespace ExchangeOfCurrencies.Data.Entities
{
    public class Credentials
    {
        public int CredentialsId { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        //public int ClientId { get; set; }
        //public Client Client { get; set; }
    }
}
