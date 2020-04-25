﻿using System;
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


namespace Employee.PersonalCard
{ 

    ///Основная форма для личной карты////
    public partial class PersonalCard_RW : Window
    {
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

                CardId = _card[15];
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
                TypeEducation = _card_education[0][0];
                DateDismissal = new DateTime();
                ReasonDismissal = "";

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
                    work_places.Add(new WorkPlace
                    {
                        DateRecruit = new DateTime(
                            Int32.Parse(_card_work[i][1].Substring(6, 4)),
                            Int32.Parse(_card_work[i][1].Substring(3, 2)),
                            Int32.Parse(_card_work[i][1].Substring(0, 2))),
                        SubDivision = _card_work[i][2],
                        Post =_card_work[i][3],
                        CharWork = _card_work[i][4],
                        TypeWork = _card_work[i][5],
                        Pay = _card_work[i][6],
                        Base = _card_work[i][7],
                    });
                }
                WorkPlaces = work_places;
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
        }

        /*Конструктор формы*/
        public PersonalCard_RW()
        {

            // создаем личную карту
            personal_card = new PersonalCard();

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

            label_num.Content = "N - " + personal_card.TablelNumber;
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

            // Пока не знаю, нодо ли это добавлять в таблицы
            //List<string> languages = dbRouteen.GetAllLanguages();   // ПОДКАЧКА СПРАВОЧНИКА НАЗВАНИЯ ЯЗЫКОВ
            //List<string> degrees = dbRouteen.GetAllDegreesLan();    // ПОДКАЧКА СПРАВОЧНИКА НАЗВАНИЯ ЯЗЫКОВ
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

        /*Выбор карточки*/
        private void ChouseBtn_Click(object sender, RoutedEventArgs e)
        {
            // соеденяемся с БД
            PersonalCard_dbRouteen dbRouteen = new PersonalCard_dbRouteen(DataBase.dbConnect.StartConnection());

            // получаем инфо личной карты
            card = dbRouteen.GetPersonalCardForID("2");
            card_lang = dbRouteen.GetLangsForID(card[1]);
            card_education = dbRouteen.GetEduForID(card[1]);
            card_work = dbRouteen.GetWorksForID(card[1]);

            // создаем личную карту
            personal_card = new PersonalCard(card, card_lang, card_education, card_work);

            // заполняем поля
            label_num.Content = "N - " + personal_card.TablelNumber;
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
        }

        /*Создание новой карты*/
        private void button_Click(object sender, RoutedEventArgs e)
        {
            // создаем личную карту
            personal_card = new PersonalCard();

            // заполняем поля
            label_num.Content = "N - " + personal_card.TablelNumber;
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


            // кидаем запрос
            dbRouteen.UpdateDataInPersonalCardForID(personal_card);
        }

        /*Созздание бокса пол*/
        private void GenderCB_Loaded(object sender, RoutedEventArgs e)
        {
            GenderCB.Items.Add("Мужской");
            GenderCB.Items.Add("Женский");
            GenderCB.Items.Add("Интерсекс");
        }

        /*Созздание бокса гражданство*/
        private void CitizenshipCB_Loaded(object sender, RoutedEventArgs e)
        {
            PersonalCard_dbRouteen dbRouteen = new PersonalCard_dbRouteen(DataBase.dbConnect.StartConnection());
            List<string> citizenship = dbRouteen.GetAllNations();
            for (int i = 0; i < citizenship.Count; i++)
                CitizenshipCB.Items.Add(citizenship[i]);
        }

        /*Созздание бокса тип обучения*/
        private void TypeEducationCB_Loaded(object sender, RoutedEventArgs e)
        {
            PersonalCard_dbRouteen dbRouteen = new PersonalCard_dbRouteen(DataBase.dbConnect.StartConnection());
            List<string> edu_types = dbRouteen.GetAllEduTypes();
            for (int i = 0; i < edu_types.Count; i++)
                TypeEducationCB.Items.Add(edu_types[i]);
        }

    }
}
