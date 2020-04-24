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
                    card.Add(1, Convert.ToString(reader.GetValue(reader.GetOrdinal("pk_personal_card"))));        // pk
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
                    for (int i = 0; i < 5; i++)
                        card.Add(11 + i, PassData[i]);   // 11 - серия, 12 - номер, 13 - кем выдан, 14 - дата выдачи


                    // залушка, чтобы выводило 1 личную карту
                    return card;
                }
            }

            return card;

        }

        // Метод получения карточек Языков по ID
        public List<List<string>> GetLangsForID(string id)
        {
            string sql = "Select * from `lang-card` where pk_personal_card=" + id;

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            // Словарь для передачи информации
            List<List<string>> card = new List<List<string>>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    int row = 0;
                    while (reader.Read())
                    {
                        List<string> str = new List<string>();
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("degree_lan"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("lan"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("pk_lan"))));
                        card.Add(str);
                        row++;
                    }

                }
            }

            return card;
        }

        // Метод получения данных паспорта по ID
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
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("pas_key"))));

                    return data;
                }
                else
                    return null;
            }

        }

        
        // подкачка образования
        public List<string> GetEduForID(string id)
        {
            string sql = "Select * from EducationCard where pk_personal_card=" + id;

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

                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("type_edu"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("univer"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("spec"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("name_doc"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("seria_doc"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("number_doc"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("year_end"))));
                    data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("pk_edu_card"))));

                    return data;
                }
                else
                    return null;
            }
        }

        // Выгрузка справочника гражданств 
        public List<string> GetAllNations()
        {
            List<string> data = new List<string>();
            string sql = "Select * from nation order by name";

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("name"))));
                    }
                }
            }
            return data;
        }

        // Выгрузка справочника с типами образования
        public List<string> GetAllEduTypes()
        {
            List<string> data = new List<string>();
            string sql = "Select * from EducationType order by name";

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("name"))));
                    }
                }
            }
            return data;
        }


        // Выгрузка справочника с названиями языков
        public List<string> GetAllLanguages()
        {
            List<string> data = new List<string>();
            string sql = "Select * from Language order by name";

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("name"))));
                    }
                }
            }
            return data;
        }

        // Выгрузка справочника степеней знания
        public List<string> GetAllDegreesLan()
        {
            List<string> data = new List<string>();
            string sql = "Select * from LanguageDegree order by name";

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("name"))));
                    }
                }
            }
            return data;
        }
    }
}
