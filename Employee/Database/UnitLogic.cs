using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Employee.TimeSheet;

namespace Employee.Database
{
    class UnitLogic
    {
        private MySqlConnection connection;
        public UnitLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<Unit> GetAll() {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `Unit`";

            var result = new List<Unit>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) {

                    while (reader.Read()) {

                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_unit")));
                        string name = Convert.ToString(reader.GetValue(reader.GetOrdinal("name")));

                        result.Add(new Unit(pk, name));
                    }
                }
            }
            return result;
        }

        public Unit GetObject(int primaryKey) {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `Unit` WHERE `pk_unit` = {primaryKey}";

            Unit unit = null;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) {

                    while (reader.Read()) {
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_unit")));
                        string name = Convert.ToString(reader.GetValue(reader.GetOrdinal("name")));

                        unit = new Unit(pk, name);
                    }
                }
            }

            return unit;
        }

        public void UpdateObject(Unit model)
        {
            try {
                string sql = $"UPDATE `Unit` SET `name` = @name` WHERE `pk_unit` = {model.PrimaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                // Добавить и настроить значение для параметра.
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = model.Name;

                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e) {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void DeleteObject(int primaryKey)
        {

            try {
                string sql = $"DELETE FROM `Unit` WHERE `pk_unit` = {primaryKey}";

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = connection;
                cmd.CommandText = sql;

                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e) {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
