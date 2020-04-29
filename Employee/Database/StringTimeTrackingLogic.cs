using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Employee.TimeSheet;

using MySql.Data.MySqlClient;

namespace Employee.Database
{
    class StringTimeTrackingLogic
    {
        private MySqlConnection connection;
        public StringTimeTrackingLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<StringTimeTracking> GetAll()
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `StringTimeTracking`";

            var result = new List<StringTimeTracking>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) {

                    while (reader.Read()) {
                        int pkPersonalCard = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_personal_card")));
                        int pkResult = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("day_type")));
                        int pkTimeTracking = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_time_tracking")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_string_time_tracking")));
                        int pkFact = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("key_yavka")));

                        result.Add(new StringTimeTracking(pk, pkTimeTracking, pkPersonalCard, pkResult, pkFact));
                    }
                }
            }
            return result;
        }

        public StringTimeTracking GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `StringTimeTracking` WHERE `pk_string_time_tracking` = {primaryKey}";

            StringTimeTracking stringTimeTracking = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) {

                    while (reader.Read()) {
                        int pkPersonalCard = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_personal_card")));
                        int pkResult = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Result_Key")));
                        int pkTimeTracking = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_time_tracking")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_string_time_tracking")));
                        int pkFact = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("key_yavka")));

                        stringTimeTracking = new StringTimeTracking(pk, pkTimeTracking, pkPersonalCard, pkResult, pkFact);
                    }
                }
            }

            return stringTimeTracking;
        }

        public void UpdateObject(StringTimeTracking model)
        {
            try {
                string sql = "UPDATE `StringTimeTracking` SET `pk_personal_card`=@pkpersonal,`Result_Key`=@pkresult," +
                             $"`pk_time_tracking`=@pktime,`key_yavka`=@pkyavka WHERE `pk_string_time_tracking` = {model.PrimaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@pkresult", MySqlDbType.Int32).Value = model.PKResult;
                cmd.Parameters.Add("@pkpersonal", MySqlDbType.Int32).Value = model.PKPersonalCard;
                cmd.Parameters.Add("@pktime", MySqlDbType.Int32).Value = model.PKTimeTracking;
                cmd.Parameters.Add("@pkyavka", MySqlDbType.Int32).Value = model.PKFact;


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
                string sql = $"DELETE FROM `StringTimeTracking` WHERE `pk_string_time_tracking` = {primaryKey}";

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


        public void CreateObject(StringTimeTracking model)
        {
            try {
                string sql = "INSERT INTO `StringTimeTracking`(`pk_personal_card`, `Result_Key`, `pk_time_tracking`, `key_yavka`)" +
                             " VALUES (@pkpersonal,@pkresult,@pktime,@pkyavka)";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@pkresult", MySqlDbType.Int32).Value = model.PKResult;
                cmd.Parameters.Add("@pkpersonal", MySqlDbType.Int32).Value = model.PKPersonalCard;
                cmd.Parameters.Add("@pktime", MySqlDbType.Int32).Value = model.PKTimeTracking;
                cmd.Parameters.Add("@pkyavka", MySqlDbType.Int32).Value = model.PKFact;

                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        public List<StringTimeTracking> GetObjectByTimeTracking(int pk_time_tracking) {
            // Создать объект Command.
                MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `StringTimeTracking` WHERE `pk_time_tracking` = {pk_time_tracking}";

            List<StringTimeTracking> stringTimeTrackings = new List<StringTimeTracking>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) {

                    while (reader.Read()) {
                        int pkPersonalCard = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_personal_card")));
                        int pkResult = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Result_Key")));
                        int pkTimeTracking = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_time_tracking")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_string_time_tracking")));
                        int pkFact = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("key_yavka")));

                        stringTimeTrackings.Add(new StringTimeTracking(pk, pkTimeTracking, pkPersonalCard, pkResult, pkFact));
                    }
                }
            }

            return stringTimeTrackings;
        }
    }
}
