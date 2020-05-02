using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Employee.StaffTable;
using System.Windows;

namespace Employee.DataBase
{
    class StringStaffTableLogic
    {
        private MySqlConnection connection;

        public StringStaffTableLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<StringStaffTableViewModel> GetAll(int pkstaffingtable)
         {

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `StringStaffingTable` WHERE `pk_staffing_table`= { pkstaffingtable }";

            var result = new List<StringStaffTableViewModel>();
            // using (DbDataReader reader = cmd.ExecuteReader())
            MySqlDataReader reader = cmd.ExecuteReader();
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int _primaryKey = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_string_staffing_table")));
                        int _positionCount = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("number_staff")));
                        double _tariff = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("stavka")));
                        double _perks = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("nadbavka")));
                        string _note = reader.GetValue(reader.GetOrdinal("note")).ToString();
                        int pk_unit = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_unit")));
                        int pk_position = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_position")));
                        int _staffingTableKey = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_staffing_table")));

                        //reader.Close();
                        Unit _subdivision = null;
                        Position _position = null;

                        string Connect = "Database=base_v2;host=kozlov.pro;port=3306;UserId=employee;Password=Dwx2EI4k";
                        using (MySqlConnection connection = new MySqlConnection(Connect))
                        {
                            connection.Open();
                            var _unitLog = new UnitLogic(connection);
                            _subdivision = _unitLog.GetObject(pk_unit);

                            var _PositionLog = new PositionLogic(connection);
                            _position = _PositionLog.GetObject(pk_position);

                            connection.Close();
                        }
                        result.Add(new StringStaffTableViewModel(_primaryKey, _positionCount, _tariff, _perks, _note, _subdivision, _position, _staffingTableKey));
                    }
                }
                reader.Close();
            }
            return result;
        }

        public StringStaffTableViewModel GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `Unit` WHERE `pk_unit` = {primaryKey}";

            StringStaffTableViewModel stringStaffTable = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int _primaryKey = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_string_staffing_table")));
                        int _positionCount = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("number_staff")));
                        double _tariff = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("stavka")));
                        double _perks = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("nadbavka")));
                        string _note = reader.GetValue(reader.GetOrdinal("note")).ToString();

                        var _unitLog = new UnitLogic(MainWindow.connection);
                        Unit _subdivision = _unitLog.GetObject(Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_unit"))));

                        var _PositionLog = new PositionLogic(MainWindow.connection);
                        Position _position = _PositionLog.GetObject(Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_position"))));

                        int _staffingTableKey = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_staffing_table")));
                        stringStaffTable = new StringStaffTableViewModel(_primaryKey, _positionCount, _tariff, _perks, _note, _subdivision, _position, _staffingTableKey);

                    }
                }
                reader.Close();
            }

            return stringStaffTable;
        }

        public int GetPrimaryKey(StringStaffTableViewModel model)
        {
            try
            {
                string sql = "SELECT `pk_string_staffing_table` FROM `StringStaffingTable` WHERE `number_staff`=@number_staff AND `stavka`=@stavka AND `nadbavka`=@nadbavka AND `note`=@note AND `pk_position`=@pk_position AND `pk_unit`=@pk_unit AND `pk_staffing_table`=@pk_staffing_table";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@number_staff", MySqlDbType.Decimal).Value = model.PositionCount;
                cmd.Parameters.Add("@stavka", MySqlDbType.Decimal).Value = model.Tariff;
                cmd.Parameters.Add("@nadbavka", MySqlDbType.Decimal).Value = model.Perks;
                cmd.Parameters.Add("@note", MySqlDbType.VarChar).Value = model.Note;
                cmd.Parameters.Add("@pk_position", MySqlDbType.Int32).Value = model.Position.PrimaryKey;
                cmd.Parameters.Add("@pk_unit", MySqlDbType.Int32).Value = model.Unit.PrimaryKey;
                cmd.Parameters.Add("@pk_staffing_table", MySqlDbType.Int32).Value = model.StaffingTableKey;

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
        public void UpdateObject(StringStaffTableViewModel model)
        {
            try
            {
                string sql = $"UPDATE `StringStaffingTable` SET `number_staff` = @number_staff, `stavka` = @stavka, `nadbavka` = @nadbavka, `note` = @note, `pk_position` = @pk_position, `pk_unit` = @pk_unit, `pk_staffing_table` = @pk_staffing_table where `pk_string_staffing_table` = {model.PrimaryKey}";
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@number_staff", MySqlDbType.Decimal).Value = model.PositionCount;
                cmd.Parameters.Add("@stavka", MySqlDbType.Decimal).Value = model.Tariff;
                cmd.Parameters.Add("@nadbavka", MySqlDbType.Decimal).Value = model.Perks;
                cmd.Parameters.Add("@note", MySqlDbType.VarChar).Value = model.Note;
                cmd.Parameters.Add("@pk_position", MySqlDbType.Int32).Value = model.Position.PrimaryKey;
                cmd.Parameters.Add("@pk_unit", MySqlDbType.Int32).Value = model.Unit.PrimaryKey;
                cmd.Parameters.Add("@pk_staffing_table", MySqlDbType.Int32).Value = model.StaffingTableKey;

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
                string sql = $"DELETE FROM `StringStaffingTable` WHERE `pk_string_staffing_table` = {primaryKey}";

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

        public void CreateObject(StringStaffTableViewModel model)
        {
            try
            {
                string sql = "INSERT INTO `StringStaffingTable`(`pk_position`, `pk_unit`, `number_staff`, `stavka`, `nadbavka`, `note`, `pk_staffing_table`) VALUES (@pk_position, @pk_unit, @number_staff, @stavka, @nadbavka, @note, @pk_staffing_table)";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@number_staff", MySqlDbType.Decimal).Value = model.PositionCount;
                cmd.Parameters.Add("@stavka", MySqlDbType.Decimal).Value = model.Tariff;
                cmd.Parameters.Add("@nadbavka", MySqlDbType.Decimal).Value = model.Perks;
                cmd.Parameters.Add("@note", MySqlDbType.VarChar).Value = model.Note;
                cmd.Parameters.Add("@pk_position", MySqlDbType.Int32).Value = model.Position.PrimaryKey;
                cmd.Parameters.Add("@pk_unit", MySqlDbType.Int32).Value = model.Unit.PrimaryKey;
                cmd.Parameters.Add("@pk_staffing_table", MySqlDbType.Int32).Value = model.StaffingTableKey;

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
