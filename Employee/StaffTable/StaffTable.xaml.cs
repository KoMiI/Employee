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

namespace Employee.StaffTable
{
    /// <summary>
    /// Логика взаимодействия для StaffTable.xaml
    /// </summary>
    public partial class StaffTable : Window
    {
        public StaffTable()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStaffTableItem addStaffTableItem = new AddStaffTableItem();
            addStaffTableItem.Show();
        }
    }
}
