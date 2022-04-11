namespace ExchangeOfCurrencies.Data.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public List<Currency> Currencies { get; set; } = new();
    }
}
