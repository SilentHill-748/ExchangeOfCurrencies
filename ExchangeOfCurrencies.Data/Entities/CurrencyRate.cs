namespace ExchangeOfCurrencies.Data.Entities
{
    public class CurrencyRate
    {
        public int RateId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; } = new();
    }
}
