using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data.Common;

namespace Employee
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
            string password = "employee";

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
        public static void ReadPersonalCard(MySqlConnection conn, string where)
        {
            string sql = "Select familiya, imya, otchestvo, inn from PersonalCard" + where;

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        // получаем index фамилию
                        int familiyaInd = reader.GetOrdinal("familiya");
                        string familiya = Convert.ToString(reader.GetValue(familiyaInd));

                        // дальше сразу комбинации 
                        // имя
                        string imya = Convert.ToString(reader.GetValue(reader.GetOrdinal("imya")));

                        // отчество
                        string otchestvo = Convert.ToString(reader.GetValue(reader.GetOrdinal("otchestvo")));

                        // ИНН
                        string inn = Convert.ToString(reader.GetValue(reader.GetOrdinal("inn")));


                        Console.WriteLine("--------------------");
                        Console.WriteLine("Фамилия:" + familiya);
                        Console.WriteLine("Имя:" + imya);
                        Console.WriteLine("Отчество:" + otchestvo);
                        Console.WriteLine("ИНН:" + inn);
                    }
                }
            }

        }
    }
}
