using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Common;
using Employee.PersonalCard;

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
                    card.Add(1, Convert.ToString(reader.GetValue(reader.GetOrdinal("pk_personal_card"))));      // pk
                    card.Add(2, Convert.ToString(reader.GetValue(reader.GetOrdinal("familiya"))));              // получаем фамилию
                    card.Add(3, Convert.ToString(reader.GetValue(reader.GetOrdinal("imya"))));                  // получаем имя
                    card.Add(4, Convert.ToString(reader.GetValue(reader.GetOrdinal("otchestvo"))));             // получаем отчество
                    card.Add(5, Convert.ToString(reader.GetValue(reader.GetOrdinal("inn"))));                   // получаем ИНН
                    card.Add(6, Convert.ToString(reader.GetValue(reader.GetOrdinal("tabel_number"))));          // получаем табельный номер
                    card.Add(7, Convert.ToString(reader.GetValue(reader.GetOrdinal("b_place"))));               // место рождения
                    card.Add(8, Convert.ToString(reader.GetValue(reader.GetOrdinal("Sex"))));                   // пол
                    card.Add(9, Convert.ToString(reader.GetValue(reader.GetOrdinal("insurance"))));             // страховое
                    card.Add(10, Convert.ToString(reader.GetValue(reader.GetOrdinal("Nation"))));               // национальность
                    card.Add(16, Convert.ToString(reader.GetValue(reader.GetOrdinal("date_create"))));           // дата создания
                    card.Add(17, Convert.ToString(reader.GetValue(reader.GetOrdinal("birthday"))));              // др

                    // читаем данные для вспомогательных функций
                    string pas_key = Convert.ToString(reader.GetValue(reader.GetOrdinal("pas_key")));

                    reader.Close();
                    // отправляем ID паспорта во вспомогательнцю функцию
                    List<string> PassData = GetPassportForID(pas_key);
                    // ДАННЫЕ ПАСПОРТА
                    for (int i = 0; i < 5; i++)
                        card.Add(11 + i, PassData[i]);   // 11 - серия, 12 - номер, 13 - кем выдан, 14 - дата выдачи


                    // залушка, чтобы выводило 1 личную карту
                    if(id != "")
                        return card;
                }
            }

            return card;

        }

        // Чтение персональных карточек
        public List<Dictionary<int, string>> GetPersonalCarAll()
        {
            string sql = "Select * from PersonalCard ";

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            // Словарь для передачи информации
            List<Dictionary<int, string>> card = new List<Dictionary<int, string>>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    Dictionary<int, string> dic = new Dictionary<int, string>();

                    dic.Add(1, Convert.ToString(reader.GetValue(reader.GetOrdinal("pk_personal_card"))));      // pk
                    dic.Add(2, Convert.ToString(reader.GetValue(reader.GetOrdinal("familiya"))));              // получаем фамилию
                    dic.Add(3, Convert.ToString(reader.GetValue(reader.GetOrdinal("imya"))));                  // получаем имя
                    dic.Add(4, Convert.ToString(reader.GetValue(reader.GetOrdinal("otchestvo"))));             // получаем отчество
                    dic.Add(5, Convert.ToString(reader.GetValue(reader.GetOrdinal("inn"))));                   // получаем ИНН
                    dic.Add(6, Convert.ToString(reader.GetValue(reader.GetOrdinal("tabel_number"))));          // получаем табельный номер
                    dic.Add(8, Convert.ToString(reader.GetValue(reader.GetOrdinal("Sex"))));                   // пол
                    dic.Add(16, Convert.ToString(reader.GetValue(reader.GetOrdinal("date_create"))));          // дата создания

                    card.Add(dic);
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
                    while (reader.Read())
                    {
                        List<string> str = new List<string>();
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("degree_lan"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("lan"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("pk_lan"))));
                        card.Add(str);
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

        // подкачка приёма/перевода
        public List<List<string>> GetWorksForID(string id)
        {
            string sql = "Select * from PersonalCardPriem where pk_personal_card=" + id;

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
                    while (reader.Read())
                    {
                        List<string> str = new List<string>();
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("pk_working"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("date"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("unit"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("position"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("work_character"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("work_type"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("taxes"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("reason"))));
                        card.Add(str);
                    }

                }
            }

            return card;
        }


        // подкачка образования
        public List<List<string>> GetEduForID(string id)
        {
            string sql = "Select * from EducationCard where pk_personal_card=" + id;

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            // Словарь для передачи информации
            List<List<string>> data = new List<List<string>>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        List<string> str = new List<string>();
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("type_edu"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("univer"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("spec"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("name_doc"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("seria_doc"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("number_doc"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("year_end"))));
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("pk_edu_card"))));
                        data.Add(str);
                    }
                    
                }
            }
            return data;
        }

        /*
        *  СПРАВОЧНИКИ
        */

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

        /*
         * 
         *  Обновление данных в PersonalCard
         * 
         */
         public void UpdateDataInPersonalCardForID(PersonalCard_RW.PersonalCard toUpdate)
        {
            string sql = "UPDATE `PersonalCard` SET "+
                        "`familiya`='"+ toUpdate.FIO.Split(' ')[0] +    
                        "' ,`imya`='" + toUpdate.FIO.Split(' ')[1] +
                        "' ,`otchestvo`='" + toUpdate.FIO.Split(' ')[2]+
                        "' WHERE pk_personal_card="+toUpdate.CardId;

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();
            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();
        }
    }
}
