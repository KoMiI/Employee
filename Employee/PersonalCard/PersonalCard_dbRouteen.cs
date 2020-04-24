using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Employee.DataBase
{
    class PersonalCard_dbRouteen
    {
        public MySqlConnection conn;
        public PersonalCard_dbRouteen(MySqlConnection _new_conn)
        {
            conn = _new_conn;
        }
        // Чтение персональной карточки по ID
        // ПОЛЯ familiya, imya, otchestvo, inn
        public Dictionary<int, string> GetPersonalCardForID(string id)
        {
            string sql = "Select * from PersonalCard where pk_personal_card=" + id;

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            // Словарь для передачи информации
            Dictionary<int, string> card = new Dictionary<int, string>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    card.Add(1, Convert.ToString(reader.GetOrdinal("familiya")));                   // получаем index фамилии
                    card.Add(2, Convert.ToString(reader.GetValue(reader.GetOrdinal("familiya"))));  // получаем фамилию
                    card.Add(3, Convert.ToString(reader.GetValue(reader.GetOrdinal("imya"))));      // получаем имя
                    card.Add(4, Convert.ToString(reader.GetValue(reader.GetOrdinal("otchestvo")))); // получаем отчество
                    card.Add(5, Convert.ToString(reader.GetValue(reader.GetOrdinal("inn"))));       // получаем ИНН
                    card.Add(6, Convert.ToString(reader.GetValue(reader.GetOrdinal("tabel_number"))));  //получаем табельный номер
                    card.Add(7, Convert.ToString(reader.GetValue(reader.GetOrdinal("b_place"))));                    // место рождения
                    card.Add(8, Convert.ToString(reader.GetValue(reader.GetOrdinal("Sex"))));                   // пол
                    card.Add(9, Convert.ToString(reader.GetValue(reader.GetOrdinal("insurance"))));
                    card.Add(10, Convert.ToString(reader.GetOrdinal("Nation")));                        // национальность

                    // читаем данные для вспомогательных функций
                    string pas_key = Convert.ToString(reader.GetValue(reader.GetOrdinal("pas_key")));

                    reader.Close();
                    // отправляем ID паспорта во вспомогательнцю функцию
                    List<string> PassData = GetPassportForID(pas_key);
                    // ДАННЫЕ ПАСПОРТА
                    for(int i=0; i<4; i++)
                        card.Add(11+i, PassData[i]);   // 11 - серия, 12 - номер, 13 - кем выдан, 14 - дата выдачи


                    // залушка, чтобы выводило 1 личную карту
                    return card;
                }
            }

            return card;

        }
        public List<string> GetPassportForID(string id)
        {
            string sql = "Select * from Pasport where pas_key=" + id;

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            // Словарь для передачи информации
            List<string> data = new List<string>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("seria"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("number"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("source"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("date_v"))));

                    return data;
                }
                else
                    return null;
            }

        }
    }
}
