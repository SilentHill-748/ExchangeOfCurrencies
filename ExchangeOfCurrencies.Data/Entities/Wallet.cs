namespace ExchangeOfCurrencies.Data.Entities
{
    public class Wallet
    {
        public Wallet()
        {
            Currencies = new List<Currency>();
        }


        public int WalletId { get; set; }
        public List<Currency> Currencies { get; set; }
    }
}
