using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopContent_Radosteva.Classes
{
    public class Connection
    {
        public static readonly string config = "server=localhost;uid=root;database=pr27;";

        public static MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(config);
            connection.Open();
            return connection;
        }

        public static MySqlDataReader Query(string SQL, MySqlConnection connection)
        {
            connection.Open();
            return new MySqlCommand(SQL, connection).ExecuteReader();
        }

        public static void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
            MySqlConnection.ClearPool(connection);
        }
    }
}
