using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.StaffTable;

namespace Employee.DataBase
{
    class OrderTypeLogic
    {
        private MySqlConnection connection;

        public OrderTypeLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<OrderType> GetAll()
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `order_type`";

            var result = new List<OrderType>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Key_Prikaz_type")));
                        string name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));

                        result.Add(new OrderType(pk, name));
                    }
                }
                reader.Close();
            }
            return result;
        }

        public OrderType GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();
            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `order_type` WHERE `Key_Prikaz_type` = {primaryKey}";
            OrderType result = null;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Key_Prikaz_type")));
                        string name = Convert.ToString(reader.GetValue(reader.GetOrdinal("Name")));
                        result = new OrderType(pk, name);
                    }
                }
                reader.Close();

            }
            return result;
        }

        public void UpdateObject(OrderType model)
        {
            try
            {
                string sql = $"UPDATE `order_type` SET `Name` = @name,` WHERE `Key_Prikaz_type` = {model.PrimaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = model.Name;

                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void DeleteObject(int primaryKey)
        {

            try
            {
                string sql = $"DELETE FROM `order_type` WHERE `Key_Prikaz_type` = {primaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
