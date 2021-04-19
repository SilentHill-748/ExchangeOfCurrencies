namespace ExchangeOfCurrencies.DbClient
{
    /// <summary>
    ///     Объект, содержащий необходимые для работы программы константы.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///     Строка для установки соединения с MySql Server.
        /// </summary>
        public const string ConnectionString = "Server = 127.0.0.1; " +
                                               "Database=exchenge_rates; " +
                                               "user=root; " +
                                               "password=K20PQR3256qsh1";
    }
}
