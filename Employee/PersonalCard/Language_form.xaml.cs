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
    ///Основная форма для знания языков////
    public partial class Language_form : Window
    {
        /*Класс, отвечающий за информацию о языке*/
        public class Lang
        {
            public string NameLang { get; set; }
            public string DegreeLang { get; set; }
        }

        /*Конструктор формы*/
        public Language_form()
        {
            InitializeComponent();
            List<Lang> lang = new List<Lang>();

            //Для проверки записей
            for (int i = 0; i < 50; i++)
                lang.Add(new Lang()
                {
                    NameLang = "Русский" + (i+1),
                    DegreeLang = "Свободно",
                });

            LangGrid.ItemsSource = lang;
        }

        /*Нажатие на кнопку сохранения*/
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
