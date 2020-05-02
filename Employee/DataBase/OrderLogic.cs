using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Employee.DataBase
{
    class OrderLogic
    {
        private MySqlConnection connection;

        public OrderLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<OrderViewModel> GetAll()
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `Order`";

            var result = new List<OrderViewModel>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_order")));
                        string number = Convert.ToString(reader.GetValue(reader.GetOrdinal("nomer")));
                        DateTime date_sostav = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("data_prikaz")));
            
                        int pk_order_type = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Key_Prikaz_type")));
                        OrderType type = null;

                        string Connect = "Database=base_v2;host=kozlov.pro;port=3306;UserId=employee;Password=Dwx2EI4k";
                        using (MySqlConnection connection = new MySqlConnection(Connect))
                        {
                            connection.Open();
                            var _OrderTypeLog = new OrderTypeLogic(connection);
                            type = _OrderTypeLog.GetObject(pk_order_type);

                            connection.Close();
                        }

                        result.Add(new OrderViewModel(pk, number, date_sostav, type));
                    }
                }
                reader.Close();
            }
            return result;
        }

        public OrderViewModel GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `Order` WHERE `pk_order` = {primaryKey}";

            OrderViewModel result = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_order")));
                        string number = Convert.ToString(reader.GetValue(reader.GetOrdinal("nomer")));
                        DateTime date_sostav = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("data_prikaz")));

                        int pk_order_type = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Key_Prikaz_type")));
                        OrderType type = null;

                        string Connect = "Database=base_v2;host=kozlov.pro;port=3306;UserId=employee;Password=Dwx2EI4k";
                        using (MySqlConnection connection = new MySqlConnection(Connect))
                        {
                            connection.Open();
                            var _OrderTypeLog = new OrderTypeLogic(connection);
                            type = _OrderTypeLog.GetObject(pk_order_type);

                            connection.Close();
                        }

                        result = new OrderViewModel(pk, number, date_sostav, type);
                    }
                }
                reader.Close();
            }

            return result;
        }

        public void UpdateObject(OrderViewModel model)
        {
            try
            {
                string sql = "UPDATE `Order` SET `nomer` = @nomer,`data_prikaz` = @date," +
                             $"`Key_Prikaz_Type` = @type_key WHERE `pk_order` = {model.PrimaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@nomer", MySqlDbType.VarChar).Value = model.NumDoc;
                cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = model.DocDate;
                cmd.Parameters.Add("@type_key", MySqlDbType.Int32).Value = model.Type.PrimaryKey;

                int rowCount = cmd.ExecuteNonQuery();

                MessageBox.Show("Обновлено записей = " + rowCount);
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
                string sql = $"DELETE FROM `Order` WHERE `pk_order` = {primaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                int rowCount = cmd.ExecuteNonQuery();

                MessageBox.Show("Удалено записей = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void CreateObject(OrderViewModel model)
        {
            try
            {
                string sql = "INSERT INTO `StaffingTable`(`nomer`, `data_prikaz`, `Key_Prikaz_type`) VALUES (@nomer, @date, @type_key)";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@nomer", MySqlDbType.VarChar).Value = model.NumDoc;
                cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = model.DocDate;
                cmd.Parameters.Add("@type_key", MySqlDbType.Int32).Value = model.Type.PrimaryKey;

                int rowCount = cmd.ExecuteNonQuery();

                MessageBox.Show("Добавлено записей = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
