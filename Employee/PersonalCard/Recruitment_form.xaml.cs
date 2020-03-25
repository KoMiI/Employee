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
    /// Логика взаимодействия для Recruitment_form.xaml
    /// </summary>
    public partial class Recruitment_form : Window
    {

        public class WorkPlace
        {
            public DateTime DateRecruit { get; set; }
            public string SubDivision { get; set; }
            public string Post { get; set; }
            public string CharWork { get; set; }
            public string TypeWork { get; set; }
            public int Pay { get; set; }
            public string Base { get; set; }
        }

        public Recruitment_form()
        {
            InitializeComponent();
            List<WorkPlace> places = new List<WorkPlace>();

            //Для проверки записей
            for(int i=0; i < 50; i++)
                places.Add(new WorkPlace()
                {
                    DateRecruit = new DateTime(2001, 2, 17),
                    SubDivision = "Отдел рекламы",
                    Post = "Маркетолог",
                    CharWork = "Постоянная",
                    TypeWork = "Основная",
                    Pay = 1000 * (i + 1),
                    Base = "Приказ N" + (i + 1) + " К3"
                });

            WorksGrid.ItemsSource = places;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
