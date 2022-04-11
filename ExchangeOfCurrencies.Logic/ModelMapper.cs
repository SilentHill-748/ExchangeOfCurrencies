using System.Reflection;

using ExchangeOfCurrencies.Logic.Interfaces;

namespace ExchangeOfCurrencies.Logic
{
    public class ModelMapper : IModelMapper
    {
        public TResult Map<TResult, TModel>(TModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            TResult result = (TResult?)Activator.CreateInstance(typeof(TResult)) ??
                throw new Exception("Something wrong at mapping process"); //TODO: Нужно указать нормальное исключение!

            RewritePropertyValues(model, result);

            return result;
        }

        public IEnumerable<TResult> Map<TResult, TModel>(IEnumerable<TModel> models)
        {
            List<TResult> mapped = new();

            foreach (TModel model in models)
                mapped.Add(Map<TResult, TModel>(model));

            return mapped;
        }

        private static void RewritePropertyValues(object from, object to)
        {
            foreach (PropertyInfo toProperty in to.GetType().GetProperties())
                foreach (PropertyInfo fromProperty in from.GetType().GetProperties())
                {
                    if (toProperty.Name.Equals(fromProperty.Name))
                    {
                        toProperty.SetValue(to, fromProperty.GetValue(from));
                        break;
                    }
                }
        }
    }
}
