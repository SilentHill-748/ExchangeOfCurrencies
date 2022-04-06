using System.Text;
using System.Xml.Serialization;

using ExchangeOfCurrencies.Logic.Exceptions;
using ExchangeOfCurrencies.Logic.Services.Interfaces;

namespace ExchangeOfCurrencies.Logic.Services
{
    public class ParseService<T> : IParseService
        where T : class
    {
        public async Task<object> ParseAsync(string url)
        {
            Type serializingType = typeof(T);
            HttpClient httpClient = new();
            XmlSerializer xmlSerializer = new(serializingType);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using Stream xmlStream = await httpClient.GetStreamAsync(url);

            var result = xmlSerializer.Deserialize(xmlStream) ??
                throw new DeserializedNullObjectException(serializingType);

            return result;
        }
    }
}
