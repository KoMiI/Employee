using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Employee.DataBase;
using ClosedXML.Excel;
using Microsoft.Win32;


namespace Employee.PersonalCard
{
    ///Основная форма для личной карты////
    public partial class PersonalCard_RW : Window
    {
        private static string NameOrganisation1 = "Новосибирский филиал МНТК";
        private static string NameOrganisation2 =  "\"Микрохирургия глаза\" им.С.Н.Фёдорова";

        // Пути файлов
        private static string PathExel = Directory.GetCurrentDirectory() + @"\T-2.xlsx";
        private static string PathPrint = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);


        Dictionary<int, string> card;                                       // Информация по одной личной карте
        List<List<string>> card_lang;                                       // Информация по языкам в ЛК
        List<List<string>> card_education;                                  // Информация по образованию в ЛК
        List<List<string>> card_work;                                       // Информация по работе в ЛК
        PersonalCard personal_card;                                         // Личная карта

        /*Класс, отвечающий за информацию одной личной карты*/
        public class PersonalCard 
        {
            public string CardId { get; set; }                              // ID
            public DateTime DatePreparation { get; set; }                   // Дата составления
            public string TablelNumber { get; set; }                        // Табельный номер
            public string INN { get; set; }                                 // ИНН
            public string InsuranceCertificate{ get; set; }                 // Номер страхового свидетельства
            public string FIO { get; set; }                                 // ФИО сотрудника
            public string Gender { get; set; }                              // Пол сотрудника
            public DateTime DateBirth { get; set; }                         // Дата рождения
            public string PlaceBirth { get; set; }                          // Место рождения
            public string Citizenship { get; set; }                         // Гражданство
            public List<Lang> Langs { get; set; }                           // Знание языков
            public string PassportPK { get; set; }                          // PK паспорта
            public string PassportNumner { get; set; }                      // Номер паспорта
            public string PassportSerial { get; set; }                      // Серия паспорта
            public DateTime PassportDate { get; set; }                      // Дата выдачи паспорта
            public string PassportIssued { get; set; }                      // Кем выдан паспорт
            public string TypeEducation { get; set; }                       // Образование
            public List<Education> Educations { get; set; }                 // Сведения об образованиях
            public List<WorkPlace> WorkPlaces { get; set; }                 // Сведения о приеме / переводе
            public DateTime DateDismissal { get; set; }                     // Дата увольнения
            public string ReasonDismissal { get; set; }                     // Основание увольнения

            public PersonalCard()
            {
                DatePreparation = new DateTime();
                TablelNumber = "";
                INN = "";
                InsuranceCertificate = "";
                FIO = "";
                Gender = "";
                DateBirth = new DateTime();
                PlaceBirth = "";
                Citizenship = "";
                PassportNumner = "";
                PassportSerial = "";
                PassportDate = new DateTime();
                PassportIssued = "";
                TypeEducation = "";
                DateDismissal = new DateTime();
                ReasonDismissal = "";

                Langs = new List<Lang>();
                Educations = new List<Education>();
                WorkPlaces = new List<WorkPlace>();
            }

            public PersonalCard(Dictionary<int, string> _card, List<List<string>> _card_lang, List<List<string>> _card_education, List<List<string>> _card_work) 
            {
                List<Lang> langs = new List<Lang>();
                List<Education> educations = new List<Education>();
                List<WorkPlace> work_places = new List<WorkPlace>();

                CardId = _card[1];
                DatePreparation = new DateTime(
                    Int32.Parse(_card[16].Substring(6, 4)),
                    Int32.Parse(_card[16].Substring(3, 2)),
                    Int32.Parse(_card[16].Substring(0, 2)));
                TablelNumber = _card[6];
                INN = _card[5];
                InsuranceCertificate = _card[9];
                FIO = _card[2] + " " + _card[3] + " " + _card[4];

                if (_card[8][0] == 'М')
                    Gender = "Мужской";
                else if (_card[8][0] == 'Ж')
                    Gender = "Женский";
                else if (_card[8][0] == 'И')
                    Gender = "Интерсекс";

                DateBirth = new DateTime(
                    Int32.Parse(_card[17].Substring(6, 4)),
                    Int32.Parse(_card[17].Substring(3, 2)),
                    Int32.Parse(_card[17].Substring(0, 2)));
                PlaceBirth = _card[7];
                Citizenship = _card[10];
                PassportNumner = _card[12];
                PassportSerial = _card[11];
                PassportDate = new DateTime(
                    Int32.Parse(_card[14].Substring(6, 4)),
                    Int32.Parse(_card[14].Substring(3, 2)),
                    Int32.Parse(_card[14].Substring(0, 2)));
                PassportIssued = _card[13];
                TypeEducation = _card[18];
                PassportPK = _card[15];

                for(int i = 0; i < _card_lang.Count; i++)
                {
                    langs.Add(new Lang
                    {
                        LangId = _card_lang[i][2],
                        NameLang = _card_lang[i][1],
                        DegreeLang = _card_lang[i][0],

                    });
                }
                Langs = langs;

                for (int i = 0; i < _card_education.Count; i++)
                {
                    educations.Add(new Education
                    {
                        EducationId = _card_education[i][7],
                        EduName = _card_education[i][1],
                        EduSpecial = _card_education[i][2],
                        EduDocName = _card_education[i][3],
                        EduDocSer = _card_education[i][4],
                        EduDocNum = _card_education[i][5],
                        DateFinal = new DateTime(
                            Int32.Parse(_card_education[i][6].Substring(6, 4)),
                            Int32.Parse(_card_education[i][6].Substring(3, 2)),
                            Int32.Parse(_card_education[i][6].Substring(0, 2))),
                    });
                }
                Educations = educations;

                for (int i = 0; i < _card_work.Count; i++)
                {
                    if (_card_work[i][9] != "") {
                        work_places.Add(new WorkPlace
                        {
                            DateRecruit = new DateTime(
                                Int32.Parse(_card_work[i][1].Substring(6, 4)),
                                Int32.Parse(_card_work[i][1].Substring(3, 2)),
                                Int32.Parse(_card_work[i][1].Substring(0, 2))),
                            SubDivision = _card_work[i][2],
                            Post = _card_work[i][3],
                            CharWork = _card_work[i][4],
                            TypeWork = _card_work[i][5],
                            Pay = _card_work[i][6],
                            Base = _card_work[i][7],

                            WorkDateDismissal = new DateTime(
                                    Int32.Parse(_card_work[i][8].Substring(6, 4)),
                                    Int32.Parse(_card_work[i][8].Substring(3, 2)),
                                    Int32.Parse(_card_work[i][8].Substring(0, 2))),
                            WorkReasonDismissal = _card_work[i][9],
                        });
                    }
                    else {
                        work_places.Add(new WorkPlace
                        {
                            DateRecruit = new DateTime(
                                Int32.Parse(_card_work[i][1].Substring(6, 4)),
                                Int32.Parse(_card_work[i][1].Substring(3, 2)),
                                Int32.Parse(_card_work[i][1].Substring(0, 2))),
                            SubDivision = _card_work[i][2],
                            Post = _card_work[i][3],
                            CharWork = _card_work[i][4],
                            TypeWork = _card_work[i][5],
                            Pay = _card_work[i][6],
                            Base = _card_work[i][7],

                            WorkDateDismissal = new DateTime(),
                            WorkReasonDismissal = "",
                        });
                    }
                }



                WorkPlaces = work_places;

                if(WorkPlaces.Last().WorkReasonDismissal != ""){
                    DateDismissal = WorkPlaces.Last().WorkDateDismissal;
                    ReasonDismissal = WorkPlaces.Last().WorkReasonDismissal;
                }
            }

        }

        /*Класс, отвечающий за информацию о языке*/
        public class Lang
        {
            public string LangId { get; set; }                              // ID
            public string NameLang { get; set; }                            // Название языка
            public string DegreeLang { get; set; }                          // Степень знания языка
        }

        /*Класс, отвечающий за информацию об образовании*/
        public class Education
        {
            public string EducationId { get; set; }                         // ID
            public string EduName { get; set; }                             // Названия заведения
            public string EduSpecial { get; set; }                          // Направление
            public string EduDocName { get; set; }                          // Наименование документа
            public string EduDocSer { get; set; }                           // Серия документа
            public string EduDocNum { get; set; }                           // Номер документа
            public DateTime DateFinal { get; set; }                         // Дата окончания
        }

        /*Класс, отвечающий за информацию о рабочем месте*/
        public class WorkPlace
        {
            public DateTime DateRecruit { get; set; }                       // Дата приема
            public string SubDivision { get; set; }                         // Подразделение
            public string Post { get; set; }                                // Должность
            public string CharWork { get; set; }                            // Характер работы
            public string TypeWork { get; set; }                            // Вид работы
            public string Pay { get; set; }                                 // Тариф / оклад
            public string Base { get; set; }                                // Основание
            public DateTime WorkDateDismissal { get; set; }
            public string WorkReasonDismissal { get; set; }
        }

        /*Создание существующей карты и заполение полей формы*/
        public void CreateFullCard(string ID)
        {
            // соеденяемся с БД
            PersonalCard_dbRouteen dbRouteen = new PersonalCard_dbRouteen(DataBase.dbConnect.StartConnection());

            // получаем инфо личной карты
            card = dbRouteen.GetPersonalCardForID(ID);
            card_lang = dbRouteen.GetLangsForID(card[1]);
            card_education = dbRouteen.GetEduForID(card[1]);
            card_work = dbRouteen.GetWorksForID(card[1]);

            // создаем личную карту
            personal_card = new PersonalCard(card, card_lang, card_education, card_work);

            // заполняем поля
            label_name.Content = "Личная карта N - " + personal_card.TablelNumber + " от " + personal_card.DatePreparation.ToString("dd.MM.yyyy");
            DatePreparationDP.SelectedDate = personal_card.DatePreparation;
            TablelNumberTB.Text = personal_card.TablelNumber;
            INN_TB.Text = personal_card.INN;
            InsuranceCertificateTB.Text = personal_card.InsuranceCertificate;
            FIO_TB.Text = personal_card.FIO;
            GenderCB.SelectedItem = personal_card.Gender;
            CitizenshipCB.SelectedItem = personal_card.Citizenship;
            DateBirthDP.SelectedDate = personal_card.DateBirth;
            PlaceBirthTB.Text = personal_card.PlaceBirth;
            LangGrid.ItemsSource = personal_card.Langs;
            PassportNumnerTB.Text = personal_card.PassportNumner;
            PassportSerialTB.Text = personal_card.PassportSerial;
            PassportDateDP.SelectedDate = personal_card.PassportDate;
            PassportIssuedTB.Text = personal_card.PassportIssued;
            TypeEducationCB.SelectedItem = personal_card.TypeEducation;
            EduGrid.ItemsSource = personal_card.Educations;
            WorksGrid.ItemsSource = personal_card.WorkPlaces;
            DismissalDP.SelectedDate = personal_card.DateDismissal;
            ReasonDismissalTB.Text = personal_card.ReasonDismissal;

            ActivateBtn();
        }


        /*Создание новой карты и заполение полей формы*/
        public void CreateNewCard()
        {
            // создаем личную карту
            personal_card = new PersonalCard();

            // заполняем поля
            label_name.Content = "Личная карта";
            DatePreparationDP.SelectedDate = personal_card.DatePreparation;
            TablelNumberTB.Text = personal_card.TablelNumber;
            INN_TB.Text = personal_card.INN;
            InsuranceCertificateTB.Text = personal_card.InsuranceCertificate;
            FIO_TB.Text = personal_card.FIO;
            GenderCB.SelectedValue = personal_card.Gender;
            CitizenshipCB.SelectedValue = personal_card.Citizenship;
            DateBirthDP.SelectedDate = personal_card.DateBirth;
            PlaceBirthTB.Text = personal_card.PlaceBirth;
            LangGrid.ItemsSource = personal_card.Langs;
            PassportNumnerTB.Text = personal_card.PassportNumner;
            PassportSerialTB.Text = personal_card.PassportSerial;
            PassportDateDP.SelectedDate = personal_card.PassportDate;
            PassportIssuedTB.Text = personal_card.PassportIssued;
            TypeEducationCB.SelectedValue = personal_card.TypeEducation;
            EduGrid.ItemsSource = personal_card.Educations;
            WorksGrid.ItemsSource = personal_card.WorkPlaces;
            DismissalDP.SelectedDate = personal_card.DateDismissal;
            ReasonDismissalTB.Text = personal_card.ReasonDismissal;

            ActivateBtn();
        }


        /*Конструктор формы*/
        public PersonalCard_RW()
        {

            InitializeComponent();
            TablelNumberTB.TextWrapping = TextWrapping.NoWrap;
            INN_TB.TextWrapping = TextWrapping.NoWrap;
            InsuranceCertificateTB.TextWrapping = TextWrapping.NoWrap;
            FIO_TB.TextWrapping = TextWrapping.NoWrap;
            PlaceBirthTB.TextWrapping = TextWrapping.NoWrap;
            PassportNumnerTB.TextWrapping = TextWrapping.NoWrap;
            PassportSerialTB.TextWrapping = TextWrapping.NoWrap;
            PassportIssuedTB.TextWrapping = TextWrapping.NoWrap;
            ReasonDismissalTB.TextWrapping = TextWrapping.NoWrap;

            CreateNewCard();

            // ПОКА НЕ ЗНАЮ КАК ДОБАВИТЬ ЭТО В ТАБЛИЦЫ, СЛОЖНААА
            //List<string> languages = dbRouteen.GetAllLanguages();   // ПОДКАЧКА СПРАВОЧНИКА НАЗВАНИЯ ЯЗЫКОВ
            //List<string> degrees = dbRouteen.GetAllDegreesLan();    // ПОДКАЧКА СПРАВОЧНИКА НАЗВАНИЯ ЯЗЫКОВ
        }

        /*Кнопка для выбора карточки*/
        private void ChouseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ChooseForm winC = new ChooseForm(this);
            winC.Closed += Window_Closed;
            winC.Show();
        }

        /*Кнопка для создания новой карты*/
        private void button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewCard();
        }

        /*Сохранение изменений карты*/
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // соеденяемся с БД
            PersonalCard_dbRouteen dbRouteen = new PersonalCard_dbRouteen(DataBase.dbConnect.StartConnection());

            // шапка
            personal_card.DatePreparation = DatePreparationDP.DisplayDate;
            personal_card.TablelNumber = TablelNumberTB.Text; 
            personal_card.INN = INN_TB.Text;
            personal_card.InsuranceCertificate = InsuranceCertificateTB.Text;
            personal_card.FIO = FIO_TB.Text;
            personal_card.Gender = GenderCB.Text;
            personal_card.Citizenship = CitizenshipCB.Text;

            // паспорт
            personal_card.PassportNumner = PassportNumnerTB.Text;
            personal_card.PassportSerial = PassportSerialTB.Text;
            personal_card.PassportDate = PassportDateDP.DisplayDate;
            personal_card.PassportIssued = PassportIssuedTB.Text;

            // образование
            personal_card.TypeEducation = TypeEducationCB.Text;

            // увольнение
            personal_card.DateDismissal = DismissalDP.DisplayDate;
            personal_card.ReasonDismissal = ReasonDismissalTB.Text;

            ActivateBtn();

            // кидаем запрос
            dbRouteen.UpdateDataInPersonalCardForID(personal_card);
        }

        /*Создание бокса пол*/
        private void GenderCB_Loaded(object sender, RoutedEventArgs e)
        {
            GenderCB.Items.Add("Мужской");
            GenderCB.Items.Add("Женский");
            GenderCB.Items.Add("Интерсекс");
        }

        /*Создание бокса гражданство*/
        private void CitizenshipCB_Loaded(object sender, RoutedEventArgs e)
        {
            PersonalCard_dbRouteen dbRouteen = new PersonalCard_dbRouteen(DataBase.dbConnect.StartConnection());
            List<string> citizenship = dbRouteen.GetAllNations();
            for (int i = 0; i < citizenship.Count; i++)
                CitizenshipCB.Items.Add(citizenship[i]);
        }

        /*Создание бокса тип обучения*/
        private void TypeEducationCB_Loaded(object sender, RoutedEventArgs e)
        {
            PersonalCard_dbRouteen dbRouteen = new PersonalCard_dbRouteen(DataBase.dbConnect.StartConnection());
            List<string> edu_types = dbRouteen.GetAllEduTypes();
            for (int i = 0; i < edu_types.Count; i++)
                TypeEducationCB.Items.Add(edu_types[i]);
        }

        /*Сохранение документа на печать*/
        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            XLWorkbook workbook = new XLWorkbook(PathExel);
            IXLWorksheet list1 = workbook.Worksheets.Worksheet(1);
            IXLWorksheet list2 = workbook.Worksheets.Worksheet(2);
            IXLWorksheet list3 = workbook.Worksheets.Worksheet(3);
            IXLWorksheet list4 = workbook.Worksheets.Worksheet(4);


            /// РАБОТА С ПЕРВЫМ ЛИСТОМ
            list1.Cell("A" + 7).Value = NameOrganisation1;
            list1.Cell("A" + 8).Value = NameOrganisation2;

            list1.Cell("A" + 16).Value = personal_card.DatePreparation.ToString("dd.MM.yyyy");
            list1.Cell("H" + 16).Value = personal_card.TablelNumber;
            list1.Cell("L" + 16).Value = personal_card.INN;
            list1.Cell("V" + 16).Value = personal_card.InsuranceCertificate;
            list1.Cell("AG" + 16).Value = personal_card.FIO[0];
            list1.Cell("AK" + 16).Value = personal_card.WorkPlaces.Last().CharWork;
            list1.Cell("AU" + 16).Value = personal_card.WorkPlaces.Last().TypeWork;
            list1.Cell("BH" + 16).Value = personal_card.Gender[0];

            string[] fio = personal_card.FIO.Split(new char[] { ' ' });
            list1.Cell("H" + 26).Value = fio[0];
            list1.Cell("AB" + 26).Value = fio[1];
            list1.Cell("AW" + 26).Value = fio[2];
            list1.Cell("K" + 29).Value = personal_card.DateBirth.ToString("dd.MM.yyyy");
            list1.Cell("L" + 31).Value = personal_card.PlaceBirth;
            list1.Cell("J" + 32).Value = personal_card.Citizenship;
            list1.Cell("J" + 32).Value = personal_card.Citizenship;

            int size_loop = 0;
            if (personal_card.Langs.Count >= 2)
                size_loop = 2;
            else
                size_loop = personal_card.Langs.Count;

            for (int i = 0; i < size_loop; i++)
            {
                list1.Cell("R" + (33 + (i * 2))).Value = personal_card.Langs[i].NameLang;
                list1.Cell("AJ" + (33 + (i * 2))).Value = personal_card.Langs[i].DegreeLang;
            }

            list1.Cell("J" + 37).Value = personal_card.TypeEducation;

            if (personal_card.Educations.Count >= 2)
                size_loop = 2;
            else
                size_loop = personal_card.Educations.Count;

            for (int i = 0; i < size_loop; i++)
            {
                string[] eduname = personal_card.Educations[i].EduName.Split(new char[] { ' ' });
                int size = eduname.Length / 3;

                string res1 = "";
                for (int j = 0; j < size; j++)
                    res1 += eduname[j] + " ";

                string res2 = "";
                for (int j = size; j < size * 2; j++)
                    res2 += eduname[j] + " ";

                string res3 = "";
                for (int j = size*2; j < eduname.Length; j++)
                    res3 += eduname[j] + " ";

                list1.Cell("A" + (41 + (i * 8))).Value = res1;
                list1.Cell("A" + (42 + (i * 8))).Value = res2;
                list1.Cell("A" + (43 + (i * 8))).Value = res3;

                list1.Cell("Z" + (43 + (i * 8))).Value = personal_card.Educations[i].EduDocName;
                list1.Cell("AK" + (43 + (i * 8))).Value = personal_card.Educations[i].EduDocSer;
                list1.Cell("AQ" + (43 + (i * 8))).Value = personal_card.Educations[i].EduDocNum;
                list1.Cell("A" + (46 + (i * 8))).Value = personal_card.Educations[i].EduSpecial;
                list1.Cell("Z" + (45 + (i * 8))).Value = personal_card.Educations[i].EduSpecial;
                list1.Cell("AZ" + (42 + (i * 8))).Value = personal_card.Educations[i].DateFinal.ToString("dd.MM.yyyy");
            }

            list1.Cell("I" + 66).Value = list1.Cell("AU" + 16).Value = personal_card.WorkPlaces.Last().Post;

            /// РАБОТА СО ВТОРЫМ ЛИСТОМ
            list2.Cell("K" + 24).Value = personal_card.PassportNumner + " " + personal_card.PassportSerial;

            string[] datePas = personal_card.PassportDate.ToString("dd.MM.yyyy").Split(new char[] { '.' });
            list2.Cell("AK" + 24).Value = datePas[0];
            list2.Cell("AO" + 24).Value = NumToStringDate(datePas[1]);
            list2.Cell("BC" + 24).Value = datePas[2];
            list2.Cell("H" + 25).Value = personal_card.PassportIssued;

            /// РАБОТА С ТРЕТЬИМ ЛИСТОМ
            if (personal_card.Educations.Count >= 12)
                size_loop = 12;
            else
                size_loop = personal_card.WorkPlaces.Count;

            for (int i = 0; i < size_loop; i ++)
            {
                list3.Cell("A" + (11 + i)).Value = personal_card.WorkPlaces[i].DateRecruit.ToString("dd:MM:yyyy");
                list3.Cell("H" + (11 + i)).Value = personal_card.WorkPlaces[i].SubDivision;
                list3.Cell("V" + (11 + i)).Value = personal_card.WorkPlaces[i].Post;
                list3.Cell("AJ" + (11 + i)).Value = personal_card.WorkPlaces[i].Pay;
                list3.Cell("AS" + (11 + i)).Value = personal_card.WorkPlaces[i].Base;
            }

            /// РАБОТА С ЧЕТВЕРТЫМ ЛИСТОМ
            string[] dateDis = personal_card.DateDismissal.ToString("dd.MM.yyyy").Split(new char[] { '.' });
            list4.Cell("L" + 52).Value = dateDis[0];
            list4.Cell("P" + 52).Value = NumToStringDate(dateDis[1]);
            list4.Cell("AF" + 52).Value = dateDis[2][2] + dateDis[2][3];
            list4.Cell("AE" + 49).Value = personal_card.ReasonDismissal;

            
            // Сохранение файла
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files|*.xlsx",
                Title = "Save an Excel File",
                FileName = "T-2 ~ " + personal_card.FIO + ".xlsx",
            };

            if (saveFileDialog.ShowDialog() == true && !String.IsNullOrWhiteSpace(saveFileDialog.FileName))
            {
                workbook.SaveAs(saveFileDialog.FileName);
                workbook.Dispose();
            }
        }

        /*Перевод месяца в слово*/
        public string NumToStringDate(string _date)
        {
            switch (_date)
            {
                case "01":
                    return "Январь";
                case "02":
                    return "Февраль";
                case "03":
                    return "Март";
                case "04":
                    return  "Апрель";
                case "05":
                    return "Май";
                case "06":
                    return "Июнь";
                case "07":
                    return "Июль";
                case "08":
                    return "Август";
                case "09":
                    return "Сентябрь";
                case "10":
                    return "Октябрь";;
                case "11":
                    return "Ноябрь";
                case "12":
                    return "Декабрь";
                default:
                    return _date;
            }
        }

        /*Проерка на заполненость*/
        public bool CheckForm()
        {
            if (personal_card.DatePreparation == new DateTime())  return false;
            if (personal_card.TablelNumber == "") return false;
            if (personal_card.INN == "") return false;
            if (personal_card.InsuranceCertificate == "") return false;
            if (personal_card.FIO == "") return false;
            if (personal_card.Gender == "") return false;
            if (personal_card.DateBirth == new DateTime()) return false;
            if (personal_card.PlaceBirth == "") return false;
            if (personal_card.Citizenship == "") return false;
            if (personal_card.PassportNumner == "") return false;
            if (personal_card.PassportSerial == "") return false;
            if (personal_card.PassportDate == new DateTime()) return false;
            if (personal_card.PassportIssued == "") return false;
            if (personal_card.TypeEducation == "") return false;

            if (personal_card.Langs == new List<Lang>()) return false;
            if (personal_card.Educations == new List<Education>()) return false;
            if (personal_card.WorkPlaces == new List<WorkPlace>()) return false;

            return true;
        }

        /*Активация кнопок*/
        public void ActivateBtn()
        {
            if (!CheckForm())
            {
                SaveBtn.IsEnabled = false;
                PrintBtn.IsEnabled = false;
            }
            else
            {
                SaveBtn.IsEnabled = true;
                PrintBtn.IsEnabled = true;
            }
        }

        /*Закрытие окна*/
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                Window win = ((Window)sender);
                win.Closed -= Window_Closed;
                this.Show();
            }
            catch (Exception) { this.Close(); }
        }
    }
}
