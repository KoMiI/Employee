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
    class DayLogic
    {
        private MySqlConnection connection;
        public DayLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<Day> GetAll()
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `Day`";

            var result = new List<Day>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        int pk_strtime = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_string_time_tracking")));
                        string daytype = Convert.ToString(reader.GetValue(reader.GetOrdinal("day_type")));
                        int duration = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("duration")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("day_key")));
                        DateTime date = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("date")));

                        result.Add(new Day(pk, daytype, duration, pk_strtime, date));
                    }
                }
            }
            return result;
        }

        public Day GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `Day` WHERE `day_key` = {primaryKey}";

            Day day = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) {

                    while (reader.Read()) {
                        int pk_strtime = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_string_time_tracking")));
                        string daytype = Convert.ToString(reader.GetValue(reader.GetOrdinal("day_type")));
                        int duration = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("duration")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("day_key")));
                        DateTime date = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("date")));

                        day = new Day(pk, daytype, duration, pk_strtime, date);
                    }
                }
            }

            return day;
        }

        public void UpdateObject(Day model)
        {
            try
            {
                string sql = "UPDATE `Day` SET `pk_string_time_tracking` = @strPK, `day_type` = @daytype, " +
                             $"`duration` = @duration, `date` = @date WHERE `key_yavka` = {model.PrimaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@strPK", MySqlDbType.Int32).Value = model.StringPrimaryKey;
                cmd.Parameters.Add("@duration", MySqlDbType.Int32).Value = model.DurationWork;
                cmd.Parameters.Add("@daytype", MySqlDbType.VarChar).Value = model.DayType;
                cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = model.Date;


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
                string sql = $"DELETE FROM `Day` WHERE `day_key` = {primaryKey}";

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
        public long CreateObject(Day model)
        {
            try
            {
                string sql = "INSERT INTO `Day`(`day_type`, `duration`, `pk_string_time_tracking`, `date`) " +
                             "VALUES (@daytype, @duration, @strPK, @date)";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@strPK", MySqlDbType.Int32).Value = model.StringPrimaryKey;
                cmd.Parameters.Add("@duration", MySqlDbType.Int32).Value = model.DurationWork;
                cmd.Parameters.Add("@daytype", MySqlDbType.VarChar).Value = model.DayType;
                cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = model.Date;

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

        public List<Day> GetObjectByTimeTracking(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `Day` WHERE `pk_string_time_tracking` = {primaryKey}";

            List<Day> days = new List<Day>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int pk_strtime = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_string_time_tracking")));
                        string daytype = Convert.ToString(reader.GetValue(reader.GetOrdinal("day_type")));
                        int duration = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("duration")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("day_key")));
                        DateTime date = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("date")));

                        days.Add(new Day(pk, daytype, duration, pk_strtime, date));
                    }
                }
            }

            return days;
        }
    }
}
