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
    class ResultLogic
    {
        private MySqlConnection connection;
        public ResultLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<Result> GetAll()
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `result_tracking`";

            var result = new List<Result>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) {

                    while (reader.Read()) {

                        int Not_Workk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Not_Work")));
                        int Holydays = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Holydays")));
                        int Night_Hours = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Night_Hours")));
                        int Hours_Overwork = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Hours_Overwork")));
                        int Hours_Work = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Hours_Work")));
                        int Day_Work = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Day_Work")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Result_Key")));

                        result.Add(new Result(pk, Not_Workk, Holydays, Night_Hours, Hours_Overwork, Hours_Work, Day_Work));
                    }
                }
            }
            return result;
        }

        public Result GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `result_tracking` WHERE `Result_Key` = {primaryKey}";

            Result result = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) {

                    while (reader.Read()) {
                        int Not_Workk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Not_Work")));
                        int Holydays = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Holydays")));
                        int Night_Hours = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Night_Hours")));
                        int Hours_Overwork = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Hours_Overwork")));
                        int Hours_Work = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Hours_Work")));
                        int Day_Work = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Day_Work")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Result_Key")));

                        result = new Result(pk, Not_Workk, Holydays, Night_Hours, Hours_Overwork, Hours_Work, Day_Work);
                    }
                }
            }

            return result;
        }

        public void UpdateObject(Result model)
        {
            try {
                string sql = $"UPDATE `result_tracking` SET `Not_Work` = @notWork`, `Holydays` = @holydays`," +
                             $"`Night_Hours` = @nightHours`, `Hours_Overwork` = @hoursOverwork`," +
                             $"`Hours_Work` = @hoursWork`,`Day_Work` = @dayWork` WHERE `Result_Key` = {model.PrimaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@notWork", MySqlDbType.Int32).Value = model.NotWork;
                cmd.Parameters.Add("@holydays", MySqlDbType.Int32).Value = model.Holydays;
                cmd.Parameters.Add("@nightHours", MySqlDbType.Int32).Value = model.NightHours;
                cmd.Parameters.Add("@hoursOverwork", MySqlDbType.Int32).Value = model.HoursOverwork;
                cmd.Parameters.Add("@hoursWork", MySqlDbType.Int32).Value = model.HoursWork;
                cmd.Parameters.Add("@dayWork", MySqlDbType.Int32).Value = model.DayWork;


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
                string sql = $"DELETE FROM `result_tracking` WHERE `Result_Key` = {primaryKey}";

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


        public long CreateObject(Result model)
        {
            try {
                string sql = "INSERT INTO `result_tracking`(`Not_Work`, `Holydays`, `Night_Hours`, " +
                             "`Hours_Overwork`, `Hours_Work`, `Day_Work`) VALUES" +
                             " (@notWork, @holydays,@nightHours,@hoursOverwork,@hoursWork,@dayWork)";

                MySqlCommand cmd = new MySqlCommand {Connection = connection, CommandText = sql};


                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@notWork", MySqlDbType.Int32).Value = model.NotWork;
                cmd.Parameters.Add("@holydays", MySqlDbType.Int32).Value = model.Holydays;
                cmd.Parameters.Add("@nightHours", MySqlDbType.Int32).Value = model.NightHours;
                cmd.Parameters.Add("@hoursOverwork", MySqlDbType.Int32).Value = model.HoursOverwork;
                cmd.Parameters.Add("@hoursWork", MySqlDbType.Int32).Value = model.HoursWork;
                cmd.Parameters.Add("@dayWork", MySqlDbType.Int32).Value = model.DayWork;

                int rowCount = cmd.ExecuteNonQuery();
                return cmd.LastInsertedId;

                Console.WriteLine("Row Count affected = " + rowCount);
            } catch (Exception e) {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                return -1;
            }
        }
    }
}
