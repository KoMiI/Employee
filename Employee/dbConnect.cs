using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data.Common;

namespace Employee.DataBase
{
    class dbConnect
    {
        public static MySqlConnection GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
        public static MySqlConnection GetDBConnection()
        {
            string host = "kozlov.pro";
            int port = 3306;
            string database = "base_v2";
            string username = "employee";
            string password = "Dwx2EI4k";

            return GetDBConnection(host, port, database, username, password);
        }
        public static MySqlConnection StartConnection()
        {
            MySqlConnection conn;
            Console.WriteLine("Getting Connection ...");
            conn = GetDBConnection();

            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();

                Console.WriteLine("Connection successful!");
               
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Console.Read();
            return conn;
        }
    }
}
