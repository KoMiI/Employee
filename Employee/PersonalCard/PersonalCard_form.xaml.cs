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
    /// Логика взаимодействия для PersonalCard_RW.xaml
    /// </summary>
    public partial class PersonalCard_RW : Window
    {
        public PersonalCard_RW()
        {
            InitializeComponent();
        }

        private void RecruitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Recruitment_form winR = new Recruitment_form();
            winR.Closed += Window_Closed;
            winR.Show();
        }

        private void EducationBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 winE = new Window1();
            winE.Closed += Window_Closed;
            winE.Show();
        }


        private void LanguageBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Language_form winL = new Language_form();
            winL.Closed += Window_Closed;
            winL.Show();
        }

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

        private void richTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboBox_Copy6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
