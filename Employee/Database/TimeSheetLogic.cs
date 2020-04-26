using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Employee.TimeSheet;

using MySql.Data.MySqlClient;

namespace Employee.Database
{
    class TimeSheetLogic
    {
        private MySqlConnection connection;

        public TimeSheetLogic(MySqlConnection conn) {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<TimeTracking> GetAll() {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `TimeTracking`";

            var result = new List<TimeTracking>();
            using (DbDataReader reader = cmd.ExecuteReader()) {
                if (reader.HasRows) {

                    while (reader.Read()) {

                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_time_tracking")));
                        string number = Convert.ToString(reader.GetValue(reader.GetOrdinal("nomer")));
                        DateTime date_sostav = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("date_sostav")));
                        DateTime from = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("from")));
                        DateTime to = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("to")));
                        int pk_unit = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_unit")));

                        result.Add(new TimeTracking(pk, number, date_sostav, from, to, pk_unit));
                    }
                }
            }
            return result;
        }

        public TimeTracking GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `TimeTracking` WHERE `pk_time_tracking` = {primaryKey}";

            TimeTracking timeTracking = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                   while (reader.Read()) {
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_time_tracking")));
                        string number = Convert.ToString(reader.GetValue(reader.GetOrdinal("nomer")));
                        DateTime date_sostav = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("date_sostav")));
                        DateTime from = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("from")));
                        DateTime to = Convert.ToDateTime(reader.GetValue(reader.GetOrdinal("to")));
                        int pk_unit = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_unit")));

                        timeTracking = new TimeTracking(pk, number, date_sostav, from, to, pk_unit);
                   }
                }
            }

            return timeTracking;
        }

        public void UpdateObject(TimeTracking model) {
            try {
                string sql = "UPDATE `TimeTracking` SET `nomer` = @nomer,`date_sostav` = @date," +
                             $"`to` = @date_to,`from` = @date_from,`pk_unit` = @pk_unit WHERE `pk_time_tracking` = {model.PrimaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@nomer", MySqlDbType.VarChar).Value = model.NumberDocument;
                cmd.Parameters.Add("@date", MySqlDbType.DateTime).Value = model.DateСompilation;
                cmd.Parameters.Add("@date_to", MySqlDbType.DateTime).Value = model.EndDate;
                cmd.Parameters.Add("@date_from", MySqlDbType.DateTime).Value = model.BeginDate;
                cmd.Parameters.Add("@pk_unit", MySqlDbType.Int32).Value = model.PKUnit;

                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void DeleteObject(int primaryKey) {
            
            try
            {
                string sql = $"DELETE FROM `TimeTracking` WHERE `pk_time_tracking` = {primaryKey}";

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
