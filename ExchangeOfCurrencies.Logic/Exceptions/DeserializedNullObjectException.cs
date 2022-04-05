namespace ExchangeOfCurrencies.Logic.Exceptions
{
    public class DeserializedNullObjectException : Exception
    {
        public DeserializedNullObjectException(Type deserializedType)
            : base()
        {
            Message = $"Object {deserializedType.Name} is null after serializing!";
        }

        public DeserializedNullObjectException(string message)
            : base(message)
        {
            Message = message;
        }


        public override string Message { get; }
    }
}
