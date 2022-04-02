namespace ExchangeOfCurrencies.Data.Entities
{
    public class Currency
    {
        public Currency()
        {
            Name = string.Empty;
            EngName = string.Empty;
            Rates = new List<CurrencyRate>();
        }


        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string EngName { get; set; }
        public List<CurrencyRate> Rates { get; set; }
    }
}
