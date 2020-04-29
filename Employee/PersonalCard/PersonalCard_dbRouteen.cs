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
                    card.Add(18, Convert.ToString(reader.GetValue(reader.GetOrdinal("type_edu"))));              // тип образования (НОВОЕ)

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
                reader.Close();
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
                    while (reader.Read())
                    {
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
                reader.Close();
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
                reader.Close();

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
                    reader.Close();

                    return data;
                }
                reader.Close();

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
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("date_fired"))));                // добавил
                        str.Add(Convert.ToString(reader.GetValue(reader.GetOrdinal("reason_fired"))));              // по увольнению
                        card.Add(str);
                    }

                }
                reader.Close();

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
                reader.Close();

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
                reader.Close();

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
                reader.Close();

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
                reader.Close();

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
                reader.Close();

            }
            return data;
        }

        /*
         * 
         *  Обновление данных в PersonalCard
         * 
         */
        public PersonalCard_RW.PersonalCard UpdateDataInPersonalCardForID(PersonalCard_RW.PersonalCard toUpdate)
        {
            string CardId = toUpdate.CardId;
            DateTime DatePreparation = toUpdate.DatePreparation;
            string TablelNumber = toUpdate.TablelNumber;
            string INN = toUpdate.INN;
            string InsuranceCertificate = toUpdate.InsuranceCertificate;
            string Gender = toUpdate.Gender;
            string[] fio = toUpdate.FIO.Split(new char[] { ' ' });
            DateTime DateBirth = toUpdate.DateBirth;
            string PlaceBirth = toUpdate.PlaceBirth;
            string Citizenship = toUpdate.Citizenship;
            string PassportPK = toUpdate.PassportPK;
            string PassportNumner = toUpdate.PassportNumner;
            string PassportSerial = toUpdate.PassportSerial;
            DateTime PassportDate = toUpdate.PassportDate;
            string PassportIssued = toUpdate.PassportIssued;
            string TypeEducation = toUpdate.TypeEducation;
            DateTime DateDismissal = toUpdate.DateDismissal;
            string ReasonDismissal = toUpdate.ReasonDismissal;

            List<PersonalCard_RW.Lang> langs = toUpdate.Langs;
            List<PersonalCard_RW.Education> educations = toUpdate.Educations;
            List<PersonalCard_RW.WorkPlace> workPlaces = toUpdate.WorkPlaces;

            string sql = "UPDATE `PersonalCard` SET " +
                        "`familiya`='" + fio[0] +
                        "' ,`imya`='" + fio[1] +
                        "' ,`otchestvo`='" + fio[2] +
                        "' ,`Sex`='" + Gender[0] +
                        "' ,`inn`='" + INN +
                        "' ,`insurance`='" + InsuranceCertificate +
                        "' ,`Nation`='" + Citizenship +
                        "' ,`tabel_number`='" + TablelNumber +
                        "' ,`b_place`='" + PlaceBirth +
                        "' ,`birthday`='" + DateBirth.ToString("yyyy'-'MM'-'dd") +
                        "' WHERE pk_personal_card=" + CardId;
            Console.WriteLine(sql);
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();
            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            // Работа с паспортом
            PassportUpdate(PassportSerial, PassportNumner, PassportIssued, PassportDate.ToString("yyyy'-'MM'-'dd"), PassportPK);


            // Работа с Языками
            for (int i = 0; i < langs.Count; i++)
            {
               
                if (langs[i].DegreeLang == "" && langs[i].NameLang == "")
                {
                    LangCardDelete(langs[i].LangId);
                    toUpdate.Langs.RemoveAt(i);
                }
                else
                {
                    if (!LangCardUpdate(langs[i]))
                    {
                        toUpdate.Langs[i].LangId = LangCardCreate(CardId, langs[i]);
                    }
                }
            }

           // educations[i].DateFinal.ToString("dd'.'MM'.'yyyy");
            // работа с образованием
            for (int i = 0; i < educations.Count; i++)
            {
                if (!EduUpdate(educations[i]))
                {
                    toUpdate.Educations[i].EducationId = EduCreate(CardId, educations[i]);
                }
            }
            
            // Карточки приёма
            for (int i = 0; i < workPlaces.Count; i++)
            {
                if (!EduUpdate(educations[i]))
                {
                    toUpdate.WorkPlaces[i].WorkPlaceID = WorkCreate(CardId, workPlaces[i]);
                }
            }

            return toUpdate;
        }

        // Пустая личная карточка
        public string EmptyPersonalCard()
        {
            string pk_pass = PassportCreate("", "", "", "2000-01-01");
            string sql = "INSERT INTO `PersonalCard` (`pas_key`) VALUES ('" + pk_pass + "'); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            object O = cmd.ExecuteScalar();
            return O.ToString();
        }

        /*
         *   ЛОГИКА КАРТОЧКИ РАБОТЫ
         * */

        public bool WorkUpdate(PersonalCard_RW.WorkPlace _card)
        {
            string sql = "UPDATE `PersonalCardPriem` SET " +
                   "`work_character`='" + _card.CharWork +
                   "', `work_type`='" + _card.TypeWork +
                   "', `position`='" + _card.Post +
                   "', `unit`='" + _card.SubDivision +
                   "', `date`='" + _card.DateRecruit.ToString("dd'.'MM'.'yyyy") +
                   "', `taxes`='" + _card.Pay +
                   "', `reason`='" + _card.Base +
                   "', `date_fired`='" + _card.WorkDateDismissal.ToString("dd'.'MM'.'yyyy") +
                   "', `reason_fired`='" + _card.WorkReasonDismissal +
                   "' WHERE pk_working=" + _card.WorkPlaceID;
            Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void WorkDelete(string pk_work)
        {
            string sql = "DELETE FROM `PersonalCardPriem` WHERE pk_edu_card=" + pk_work;
            Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        public string WorkCreate(string pk_personal_card, PersonalCard_RW.WorkPlace _card)
        {
            string sql = "INSERT INTO `PersonalCardPriem` (`work_character`, `work_type`, `position`, `unit`, `date`, `taxes`, `reason`, `pk_personal_card`, `date_fired`, `reason_fired`) " +
                "VALUES ('" + _card.CharWork+
                "', '" + _card.TypeWork +
                "', '" + _card.Post +
                "', '" + _card.SubDivision +
                "', '" + _card.DateRecruit.ToString("dd'.'MM'.'yyyy") +
                "', '" + _card.Pay +
                "', '" + _card.Base +
                "', '" + pk_personal_card+
                "'); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            object O = cmd.ExecuteScalar();
            return O.ToString();
        }


        public string EduCreate(string pk_personal_card, PersonalCard_RW.Education _card)
        {
            string sql = "INSERT INTO `EducationCard` (`univer`, `name_doc`, `number_doc`, `seria_doc`, `year_end`, `pk_personal_card`, `spec`) " +
                "VALUES ('" + _card.EduName +
                "', '" + _card.EduDocName +
                "', '" + _card.EduDocNum +
                "', '" + _card.EduDocSer +
                "', '" + _card.DateFinal.ToString("dd'.'MM'.'yyyy") +
                "', '" + pk_personal_card +
                "', '" + _card.EduSpecial +
                "'); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            object O = cmd.ExecuteScalar();
            return O.ToString();
        }

        public bool EduUpdate(PersonalCard_RW.Education _card)
        {
            string sql = "UPDATE `EducationCard` SET " +
                    "`univer`='" + _card.EduName +
                    "', `number_doc`='" + _card.EduDocNum +
                    "', `seria_doc`='" + _card.EduDocSer +
                    "', `year_end`='" + _card.DateFinal.ToString("dd'.'MM'.'yyyy") +
                    "', `name_doc`='" + _card.EduDocName +
                    "', `name_doc`='" + _card.EduDocName +
                    "', `spec`='" + _card.EduSpecial +
                    "' WHERE pk_edu_card=" + _card.EducationId;
            Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void EduDelete(string pk_edu)
        {
            string sql = "DELETE FROM `EducationCard` WHERE pk_edu_card=" + pk_edu;
            Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        public string LangCardCreate(string pk_personal_card, PersonalCard_RW.Lang _card)
        {
            string sql = "INSERT INTO `lang-card`(`pk_personal_card`, `degree_lan`, `lan`) " +
                "VALUES ('" + pk_personal_card +
                "', '" + _card.DegreeLang + 
                "', '" + _card.NameLang +
                "'); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            object O = cmd.ExecuteScalar();
            return O.ToString();
        }

        public bool LangCardUpdate(PersonalCard_RW.Lang _card)
        {
            string sql = "UPDATE `lang-card` SET " +
                   "`lan`='" + _card.NameLang +
                   "', `degree_lan`='" + _card.DegreeLang +
                   "' WHERE pk_lan=" + _card.LangId;
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
        public void LangCardDelete(string pk_lan)
        {
            string sql = "DELETE FROM `lang-card` WHERE pk_lan=" + pk_lan;
            Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }


       
        // пасспорт 
        public string PassportCreate(string seria, string number, string source, string date)
        {
            string sql = "INSERT INTO `Pasport` (`seria`, `number`, `source`, `date_v`)" +
                 "VALUES ('" + seria +
                 "', '" + number +
                 "', '" + source +
                 "', '" + date +
                 "'); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            object O = cmd.ExecuteScalar();
            return O.ToString();
        }

        public void PassportUpdate(string seria, string number, string source, string date, string pk)
        {
           string sql = "UPDATE `Pasport` SET " +
                    "`seria`='" + seria +
                    "', `number`='" + number +
                    "', `source`='" + source +
                    "', `date_v`='" + date+
                    "' WHERE pas_key=" + pk;
            Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
        }
    }
}
