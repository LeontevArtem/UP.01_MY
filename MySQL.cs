using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UP._01
{
    public class MySQL
    {
        public static MySqlDataReader Query(string query)
        {
            string connectString = "server=localhost;port=3308;database=UP.01;uid=root;";
            MySqlConnection connection = new MySqlConnection(connectString);
            if (connection.State.ToString() == "Closed")
                connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            return cmd.ExecuteReader();
        }
    }
}
