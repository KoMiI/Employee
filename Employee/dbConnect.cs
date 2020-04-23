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

        // Чтение персональной карточки ПОЛЯ familiya, imya, otchestvo, inn
        public static Dictionary<int, string> GetPersonalCard(MySqlConnection conn, string where)
        {
            string sql = "Select familiya, imya, otchestvo, inn from PersonalCard" + where;

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            // Словарь для передачи информации
            Dictionary<int, string> card = new Dictionary<int, string>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        card.Add(1, Convert.ToString(reader.GetOrdinal("familiya")));                   // получаем index фамилии
                        card.Add(2, Convert.ToString(reader.GetValue(reader.GetOrdinal("familiya"))));  // получаем фамилию
                        card.Add(3, Convert.ToString(reader.GetValue(reader.GetOrdinal("imya"))));      // получаем имя
                        card.Add(4, Convert.ToString(reader.GetValue(reader.GetOrdinal("otchestvo")))); // получаем отчество
                        card.Add(5, Convert.ToString(reader.GetValue(reader.GetOrdinal("inn"))));       // получаем ИНН

                        // залушка, чтобы выводило 1 личную карту
                        return card;
                    }
                }
            }

            return card;

        }
    }
}
