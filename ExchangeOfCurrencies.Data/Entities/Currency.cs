namespace ExchangeOfCurrencies.Data.Entities
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EngName { get; set; } = string.Empty;
        public List<CurrencyRate>? Rates { get; set; }
    }
}
