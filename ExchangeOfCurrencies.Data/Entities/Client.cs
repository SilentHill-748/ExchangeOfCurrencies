namespace ExchangeOfCurrencies.Data.Entities
{
    public class Client
    {
        public Client()
        {
            FullName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Wallet = new Wallet();
            Credentials = new Credentials();
        }


        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public int CredentialsId { get; set; }
        public Credentials Credentials { get; set; }
    }
}
