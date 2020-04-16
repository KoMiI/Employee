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
    /// <summary>
    /// Логика взаимодействия для Language_form.xaml
    /// </summary>
    public partial class Language_form : Window
    {

        public class Language
        {
            public string NameLang { get; set; }
            public string DegreeLang { get; set; }
        }

        public Language_form()
        {
            InitializeComponent();
            List<Language> lang = new List<Language>();

            //Для проверки записей
            for (int i = 0; i < 50; i++)
                lang.Add(new Language()
                {
                    NameLang = "Русский" + (i+1),
                    DegreeLang = "Свободно",
                });

            LangGrid.ItemsSource = lang;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
