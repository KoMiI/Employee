using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace Employee.TimeSheet
{
    public class PersonalCard
    {
        public int PrimaryKey { get; set; }
        public string FIO { get; set; }
        public int TableNumber { get; set; }
        public string Position { get; set; }

        public PersonalCard(int pk, string f, string n, string o, string position, int number)
        {
            FIO = $"{f} {n} {o}";
            Position = position;
            TableNumber = TableNumber;
        }

        public PersonalCard() { }
        public PersonalCard(int pk) {
            PrimaryKey = pk;
            load();
        }

        private void load() {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = MainWindow.connection;
            cmd.CommandText = "SELECT `PersonalCard`.`familiya`, `PersonalCard`.`imya`,`PersonalCard`.`tabel_number`, " +
                              "`PersonalCardPriem`.`position`, `PersonalCard`.`otchestvo` FROM `PersonalCard`, `PersonalCardPriem` " +
                              $"WHERE `PersonalCardPriem`.`pk_personal_card` = `PersonalCard`.`pk_personal_card` and `PersonalCard`.`pk_personal_card` = {PrimaryKey}";

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows) {

                    while (reader.Read()) {
                        string familiya = Convert.ToString(reader.GetValue(reader.GetOrdinal("familiya")));
                        string name = Convert.ToString(reader.GetValue(reader.GetOrdinal("imya")));
                        TableNumber = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("tabel_number")));
                        string otchestvo = Convert.ToString(reader.GetValue(reader.GetOrdinal("otchestvo")));
                        Position = Convert.ToString(reader.GetValue(reader.GetOrdinal("position")));
                        FIO = $"{familiya} {name} {otchestvo}";
                    }
                }
            }
        }
    }
}
