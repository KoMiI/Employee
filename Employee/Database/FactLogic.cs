using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Employee.TimeSheet;

using MySql.Data.MySqlClient;

namespace Employee.Database
{
    class FactLogic
    {
        private MySqlConnection connection;
        public FactLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<Fact> GetAll()
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `result_tracking`";

            var result = new List<Fact>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        int count_day = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("count_day")));
                        string reason_neyavki = Convert.ToString(reader.GetValue(reader.GetOrdinal("reason_neyavki")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("key_yavka")));

                        result.Add(new Fact(pk, reason_neyavki, count_day));
                    }
                }
            }
            return result;
        }

        public Fact GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `Yavka` WHERE `key_yavka` = {primaryKey}";

            Fact fact = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int count_day = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("count_day")));
                        string reason_neyavki = Convert.ToString(reader.GetValue(reader.GetOrdinal("reason_neyavki")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("key_yavka")));

                        fact = new Fact(pk, reason_neyavki, count_day);
                    }
                }
            }

            return fact;
        }

        public void UpdateObject(Fact model)
        {
            try {
                string sql = "UPDATE `Yavka` SET `count_day` = @countDay`, `reason_neyavki` = @reasonNeyavki` " +
                             $"WHERE `key_yavka` = {model.PrimaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@countDay", MySqlDbType.Int32).Value = model.CountDay;
                cmd.Parameters.Add("@reasonNeyavki", MySqlDbType.VarChar).Value = model.Reason;


                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
            } catch (Exception e) {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void DeleteObject(int primaryKey)
        {

            try {
                string sql = $"DELETE FROM `Yavka` WHERE `key_yavka` = {primaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
            } catch (Exception e) { 
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }


        public long CreateObject(Fact model)
        {
            try
            {
                string sql = "INSERT INTO `Yavka`(`count_day`, `reason_neyavki`)" +
                             " VALUES (@countDay,@reasonNeyavki)";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@countDay", MySqlDbType.Int32).Value = model.CountDay;
                cmd.Parameters.Add("@reasonNeyavki", MySqlDbType.VarChar).Value = model.Reason;

                int rowCount = cmd.ExecuteNonQuery();
                return cmd.LastInsertedId;

                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
        }
    }
}
