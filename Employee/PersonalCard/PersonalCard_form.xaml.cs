using System;
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
        PersonalCard personal_card;                                         // Личная карта

        /*Класс, отвечающий за информацию одной личной карты*/
        public class PersonalCard {
            public DateTime DatePreparation { get; set; }                   // Дата составления
            public string TablelNumber { get; set; }                        // Табельный номер
            public string INN { get; set; }                                 // ИНН
            public string InsuranceCertificate{ get; set; }                 // Номер страхового свидетельства
            public string FIO { get; set; }                                 // ФИО сотрудника
            public char Gender { get; set; }                                // Пол сотрудника
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
                List<Lang> langs = new List<Lang>();
                List<Education> educations = new List<Education>();
                List<WorkPlace> work_places = new List<WorkPlace>();

                DatePreparation = new DateTime();
                TablelNumber = "";
                INN = "";
                InsuranceCertificate = "";
                FIO = "";
                Gender = ' ';
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

                langs.Add(new Lang
                {
                    NameLang = "",
                    DegreeLang = "",
                });

                Langs = langs;

                educations.Add(new Education
                {
                    EduName = "",
                    EduSpecial = "",
                    EduDocName = "",
                    EduDocSer = "",
                    EduDocNum = "",
                    DateFinal = new DateTime(),
            });

                Educations = educations;


                work_places.Add(new WorkPlace
                {
                    DateRecruit = new DateTime(),
                    SubDivision = "",
                    Post = "",
                    CharWork = "",
                    TypeWork = "",
                    Pay = "",
                    Base = "",
                });

                WorkPlaces = work_places;
            }

            public PersonalCard(Dictionary<int, string> _card) 
            {
                List<Lang> langs = new List<Lang>();
                List<Education> educations = new List<Education>();
                List<WorkPlace> work_places = new List<WorkPlace>();

                DatePreparation = new DateTime(2017, 06, 04);
                TablelNumber = "104";
                INN = _card[5];
                InsuranceCertificate = "134-237-649 13";
                FIO = _card[2] + " " + _card[3] + " " + _card[4];
                Gender = 'ж';
                DateBirth = new DateTime(1975, 02, 09);
                PlaceBirth = "г. Темрюк";
                Citizenship = "Россия";
                PassportNumner = "758018";
                PassportSerial = "54 12";
                PassportDate = new DateTime(2020, 02, 14);
                PassportIssued = "ОУФМС России по Краснодарскому краю в Темрюкском р-не";
                TypeEducation = "Высшеее профессиональное";
                DateDismissal = new DateTime();
                ReasonDismissal = "";

                langs.Add(new Lang
                {
                    NameLang = "Русский",
                    DegreeLang = "Владеет свободно",
                });

                langs.Add(new Lang
                {
                    NameLang = "Английский",
                    DegreeLang = "Владеет свободно",
                });

                Langs = langs;

                educations.Add(new Education
                {
                    EduName = "Санкт-Петербургский государственный университет",
                    EduSpecial = "Лечебное дело",
                    EduDocName = "Диплом",
                    EduDocSer = "116124",
                    EduDocNum = "4523393",
                    DateFinal = new DateTime(2005, 06, 19),
                });

                Educations = educations;


                work_places.Add(new WorkPlace
                {
                    DateRecruit = new DateTime(2017, 06, 02),
                    SubDivision = "Отдел консультации",
                    Post = "Врач-консультант",
                    CharWork = "Постоянная",
                    TypeWork = "Основная",
                    Pay = "36000",
                    Base = "Приказ от 02.06.2017",
                });

                WorkPlaces = work_places;
            }

        }

        /*Класс, отвечающий за информацию о языке*/
        public class Lang
        {
            public string NameLang { get; set; }                            // Название языка
            public string DegreeLang { get; set; }                          // Степень знания языка
        }

        /*Класс, отвечающий за информацию об образовании*/
        public class Education
        {
            public string EduName { get; set; }                             // Названия заведения
            public string EduSpecial { get; set; }                          // Направление
            public string EduDocName { get; set; }                          // Наименование документа
            public string EduDocSer { get; set; }                           // Серия документа
            public string EduDocNum { get; set; }                              // Номер документа
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

            // создаем личную карту
            personal_card = new PersonalCard();
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
            // получаем инфо личной карты
            card = DataBase.dbConnect.GetPersonalCard(DataBase.dbConnect.StartConnection(), "");

            // создаем личную карту
            personal_card = new PersonalCard(card);

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
    }
}
