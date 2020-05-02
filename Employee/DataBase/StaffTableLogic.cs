﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows;
using MySql.Data.MySqlClient;
using Employee.StaffTable;

namespace Employee.DataBase
{
    class StaffTableLogic
    {
        private MySqlConnection connection;

        public StaffTableLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<StaffTableViewModel> GetAll()
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `StaffingTable`";

            var result = new List<StaffTableViewModel>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_staffing_table")));
                        int number = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("nomer")));
                        DateTime date_sostav = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("data_sostav")));
                        DateTime from = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("d_from")));
                        DateTime to = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("d_to")));

                        int pk_order = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_order")));
                        OrderViewModel order = null;
                        string Connect = "Database=base_v2;host=kozlov.pro;port=3306;UserId=employee;Password=Dwx2EI4k";
                        using (MySqlConnection connection = new MySqlConnection(Connect))
                        {
                            connection.Open();
                            var _OrdertLog = new OrderLogic(connection);
                            order = _OrdertLog.GetObject(pk_order);

                            connection.Close();
                        }

                        result.Add(new StaffTableViewModel(pk, order,number, date_sostav, from, to));
                    }
                }
                reader.Close();
            }
            return result;
        }

        public StaffTableViewModel GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM ` StaffingTable` WHERE `pk_staffing_table` = {primaryKey}";

            StaffTableViewModel staffingTable = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_staffing_table")));
                        int number = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("nomer")));
                        DateTime date_sostav = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("data_sostav")));
                        DateTime from = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("d_from")));
                        DateTime to = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("d_to")));

                        int pk_order = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_order")));
                        OrderViewModel order = null;
                        string Connect = "Database=base_v2;host=kozlov.pro;port=3306;UserId=employee;Password=Dwx2EI4k";
                        using (MySqlConnection connection = new MySqlConnection(Connect))
                        {
                            connection.Open();
                            var _OrdertLog = new OrderLogic(connection);
                            order = _OrdertLog.GetObject(pk_order);

                            connection.Close();
                        }

                        staffingTable = new StaffTableViewModel(pk, order, number, date_sostav, from, to);
                    }
                }
                reader.Close();
            }

            return staffingTable;
        }

        public void UpdateObject(StaffTableViewModel model)
        {
            try
            {
                string sql = "UPDATE `StaffingTable` SET `nomer` = @nomer,`pk_order`=@pk_order, `data_sostav` = @date," +
                             $"`d_to` = @date_to,`d_from` = @date_from WHERE `pk_staffing_table` = {model.PrimaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@nomer", MySqlDbType.VarChar).Value = model.NumDoc;
                cmd.Parameters.Add("@pk_order", MySqlDbType.Int32).Value = model.Order.PrimaryKey;
                cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = model.CreateDate;
                cmd.Parameters.Add("@date_to", MySqlDbType.DateTime).Value = model.EndDate;
                cmd.Parameters.Add("@date_from", MySqlDbType.DateTime).Value = model.StartDate;

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
                string sql = $"DELETE FROM `StaffingTable` WHERE `StaffingTable`.`pk_staffing_table` = {primaryKey}";

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

        public void CreateObject(StaffTableViewModel model)
        {
            try
            {
                string sql = "INSERT INTO `StaffingTable`(`nomer`, `pk_order`,`data_sostav`, `d_to`, `d_from`) VALUES (@nomer, @pk_order, @date, @date_to, @date_from)";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@nomer", MySqlDbType.Int32).Value = model.NumDoc;
                cmd.Parameters.Add("@pk_order", MySqlDbType.Int32).Value = model.Order.PrimaryKey;
                cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = model.CreateDate;
                cmd.Parameters.Add("@date_to", MySqlDbType.DateTime).Value = model.EndDate;
                cmd.Parameters.Add("@date_from", MySqlDbType.DateTime).Value = model.StartDate;
                
                int rowCount = cmd.ExecuteNonQuery();

                MessageBox.Show("Добавлено записей = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        public int GetNextPrimaryKey()
        {
            try
            {
                string sql = "SELECT MAX(pk_staffing_table) FROM StaffingTable";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                int nextPK = Convert.ToInt32(cmd.ExecuteScalar());

                return nextPK + 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
        }

        public int GetPrimaryKey(StaffTableViewModel model)
        {
            try
            {
                string sql = "SELECT `pk_staffing_table` FROM `StaffingTable` WHERE `nomer` = @nomer AND `pk_order`=@pk_order AND `data_sostav` = @date AND `d_to` = @date_to  AND `d_from` = @date_from";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@nomer", MySqlDbType.VarChar).Value = model.NumDoc;
                cmd.Parameters.Add("@pk_order", MySqlDbType.Int32).Value = model.Order.PrimaryKey;
                cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = model.CreateDate;
                cmd.Parameters.Add("@date_to", MySqlDbType.DateTime).Value = model.EndDate;
                cmd.Parameters.Add("@date_from", MySqlDbType.DateTime).Value = model.StartDate;

                int pk = Convert.ToInt32(cmd.ExecuteScalar());
                return pk;
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
