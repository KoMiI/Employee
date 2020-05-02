using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.StaffTable;

namespace Employee.DataBase
{
    class PositionLogic
    {
        private MySqlConnection connection;

        public PositionLogic(MySqlConnection conn)
        {
            if (conn == null)
                Console.WriteLine("Connection is null");

            connection = conn;
        }

        public List<Position> GetAll()
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM `Position`";

            var result = new List<Position>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_position")));
                        string name = Convert.ToString(reader.GetValue(reader.GetOrdinal("name")));

                        result.Add(new Position(pk, name));
                    }
                }
                reader.Close();
            }
            return result;
        }

        public Position GetObject(int primaryKey)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();
            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `Position` WHERE `pk_position` = {primaryKey}";
            Position position = null;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_position")));
                        string name = Convert.ToString(reader.GetValue(reader.GetOrdinal("name")));
                        position = new Position(pk, name);
                    }
                }
                reader.Close();
            }
            return position;
        }

        public Position GetObject(string s_name)
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();
            // Сочетать Command с Connection.
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM `Position` WHERE `name` = `{s_name}`";
            Position position = null;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_position")));
                        string name = Convert.ToString(reader.GetValue(reader.GetOrdinal("name")));
                        position = new Position(pk, name);
                    }
                }
                reader.Close();
            }
            return position;
        }

        public void UpdateObject(Position model)
        {
            try
            {
                string sql = $"UPDATE `Position` SET `name` = @name,` WHERE `pk_position` = {model.PrimaryKey}";

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
                string sql = $"DELETE FROM `Position` WHERE `pk_position` = {primaryKey}";

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
