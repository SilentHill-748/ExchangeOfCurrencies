namespace ExchangeOfCurrencies.Logic.Interfaces
{
    public interface IModelMapper
    {
        public TResult Map<TResult, TModel>(TModel model);

        public IEnumerable<TResult> Map<TResult, TModel>(IEnumerable<TModel> models);
    }
}
