using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace ExchangeOfCurrencies.DbClient
{
    public class Request
    {
        public DataSet DataSet { get; }

        public Request()
        {
            DataSet = new DataSet();
        }

        public Request(string quary) : this()
        {
            DataSet = Send(quary);
        }

        public static DataSet Send(string quary)
        {
            DataSet data = new();
            using var connect = new MySqlConnection(Constants.ConnectionString);
            if (!connect.Ping()) connect.Open();

            using var sqlCommand = new MySqlCommand(quary, connect);
            var adapter = new MySqlDataAdapter(sqlCommand);
            adapter.Fill(data);
            return data;
        }

        public async void SendAsync(string quary)
        {
            await Task.Run(() =>
            {
                using var connect = new MySqlConnection(Constants.ConnectionString);
                if (!connect.Ping()) connect.Open();

                using var sqlCommand = new MySqlCommand(quary, connect);
                var adapter = new MySqlDataAdapter(sqlCommand);
                adapter.Fill(DataSet);
            });
        }
    }
}
