using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Employee.PersonalCard
{ 

    ///Основная форма для личной карты////
    public partial class PersonalCard_RW : Window
    {
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
            public Language_form.Lang Languages { get; set; }               // Знание языков
            public int PassportNumner { get; set; }                         // Номер паспорта
            public string PassportSerial { get; set; }                      // Серия паспорта
            public DateTime PassportDate { get; set; }                      // Дата выдачи паспорта
            public string PassportIssued { get; set; }                      // Кем выдан паспорт
            public string TypeEducation { get; set; }                       // Образование
            public Education_form.EducationMem Educations { get; set; }     // Сведения об образованиях
            public Recruitment_form.WorkPlace WorkPlaces { get; set; }      // Сведения о приеме / переводе
            public DateTime DateDismissal { get; set; }                     // Дата увольнения
            public string ReasonDismissal { get; set; }                     // Основание увольнения
        }

        /*Конструктор формы*/
        public PersonalCard_RW()
        {
            InitializeComponent();
        }

        /*Нажатие на кнопку языков*/
        private void LanguageBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Language_form winL = new Language_form();
            winL.Closed += Window_Closed;
            winL.Show();
        }

        /*Нажатие на кнопку образования*/
        private void EducationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Education_form winE = new Education_form();
            winE.Closed += Window_Closed;
            winE.Show();
        }

        /*Нажатие на кнопку приема/перевода*/
        private void RecruitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Recruitment_form winR = new Recruitment_form();
            winR.Closed += Window_Closed;
            winR.Show();
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
