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
using MySql.Data.MySqlClient;

namespace Employee.Login
{
    /// <summary>
    /// Форма логина
    /// </summary>
    public partial class LoginFormWindow : Window
    {
        public static MySqlConnection connection;
        public LoginFormWindow()
        {
            connection = dbConnect.StartConnection();
            InitializeComponent();
            
        }

        private void LoginButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            StaffTable.AllStaffTables StaffTables = new StaffTable.AllStaffTables();
            StaffTables.Show();
        }
    }
}
