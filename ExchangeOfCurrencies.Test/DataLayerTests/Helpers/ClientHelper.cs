using System.Collections.Generic;

using ExchangeOfCurrencies.Data.Entities;
using ExchangeOfCurrencies.Data;

namespace ExchangeOfCurrencies.Test.DataLayerTests.Helpers
{
    internal class ClientHelper
    {
        public static Client GetClearClient()
        {
            return new Client()
            {
                Email = "test@email.com",
                Phone = "7 894 5612235",
                FullName = "Tester Client One",
            };
        }

        public static Client GetClientWithIncludes()
        {
            return new Client()
            {
                Credentials = CredentialsHelper.GetCredentials("test", "test"),
                Email = "test@email.com",
                Phone = "7 894 5612235",
                FullName = "Tester Client One",
                Wallet = WalletHelper.GetWallet()
            };
        }

        public static List<Client> GetClearClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    Email = "test1@email.com",
                    Phone = "7 123 1232235",
                    FullName = "Tester Client One",
                },
                new Client()
                {
                    Email = "test2@email.com",
                    Phone = "7 894 1354768",
                    FullName = "Tester Client Two",
                },
                new Client()
                {
                    Email = "test3@email.com",
                    Phone = "7 777 1238889",
                    FullName = "Tester Client Three",
                }
            };
        }

        public static List<Client> GetClientsWithIncludes()
        {
            return new List<Client>()
            {
                new Client()
                {
                    Credentials = CredentialsHelper.GetCredentials("Test1", "test1"),
                    Email = "test1@email.com",
                    Phone = "7 123 1232235",
                    FullName = "Tester Client One",
                    Wallet = WalletHelper.GetWallet(),
                },
                new Client()
                {
                    Credentials = CredentialsHelper.GetCredentials("Test2", "test2"),
                    Email = "test2@email.com",
                    Phone = "7 894 1354768",
                    FullName = "Tester Client Two",
                    Wallet = WalletHelper.GetWallet(),
                },
                new Client()
                {
                    Credentials = CredentialsHelper.GetCredentials("Test3", "test3"),
                    Email = "test3@email.com",
                    Phone = "7 777 1238889",
                    FullName = "Tester Client Three",
                    Wallet = WalletHelper.GetWallet(),
                }
            };
        }
    }
}
