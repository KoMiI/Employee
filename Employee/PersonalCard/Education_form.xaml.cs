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

    public partial class Window1 : Window
    {
        public class EducationMem
        {
            public string EduName { get; set; }
            public string EduSpecial{ get; set; }
            public string EduDocName{ get; set; }
            public string EduDocSer{ get; set; }
            public int EduDocNum{ get; set; }
            public DateTime DateFinal { get; set; }
        }

        public Window1()
        {
            InitializeComponent();

            List<EducationMem> places = new List<EducationMem>();

            //Для проверки записей
            for (int i = 0; i < 50; i++)
                places.Add(new EducationMem()
                {
                    EduName = "Московский государственный университет",
                    EduSpecial = "Маркетинг",
                    EduDocName = "Диплом",
                    EduDocSer = "3-3",
                    EduDocNum = i + 250001,
                    DateFinal = new DateTime(1998, 2, 13),
                });

            EduGrid.ItemsSource = places;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
