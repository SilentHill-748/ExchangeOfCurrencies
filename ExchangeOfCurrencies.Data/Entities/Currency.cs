namespace ExchangeOfCurrencies.Data.Entities
{
    public class Currency
    {
        public string ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EngName { get; set; } = string.Empty;
        public uint NumCode { get; set; }
        public string CharCode { get; set; } = string.Empty;
        public List<CurrencyRate>? Rates { get; set; }
    }
}
