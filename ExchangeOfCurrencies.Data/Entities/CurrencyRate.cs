namespace ExchangeOfCurrencies.Data.Entities
{
    public class CurrencyRate
    {
        public int RateId { get; set; }
        public DateOnly Date { get; set; }
        public decimal Value { get; set; }
    }
}
