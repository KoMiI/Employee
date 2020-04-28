using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.Common;
using Employee.DataBase;
using Employee.Login;

namespace Employee.StaffTable
{
    /// <summary>
    /// Логика взаимодействия для StaffTable.xaml
    /// </summary>
    public partial class StafTable : Window
    {
        public StaffTableViewModel MainStaffTable = null;
        public bool AdditingFlag = false;
        public StafTable()
        {
            InitializeComponent();
            MainStaffTable = new StaffTableViewModel();
        }

        private void UpdateGrid()
        {
            //if (AdditingFlag)
            {
                //StaffTableGrid.Items.Clear();
                StaffTableGrid.ItemsSource = null;
                var stringStaffTableLogic = new StringStaffTableLogic(LoginFormWindow.connection);
                var stringStaffTable = stringStaffTableLogic.GetAll(MainStaffTable.PrimaryKey);

                MainStaffTable.StaffLines.Clear();

                foreach (var stringStaff in stringStaffTable)
                {
                    MainStaffTable.StaffLines.Add(stringStaff);
                }

                StaffTableGrid.ItemsSource = MainStaffTable.StaffLines;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!AdditingFlag)
            {
                DateCreatePicker.SelectedDate = MainStaffTable.CreateDate;
                DateStartPicker.SelectedDate = MainStaffTable.StartDate;
                DateEndPicker.SelectedDate = MainStaffTable.EndDate;
                OKYDTextBox.Text = MainStaffTable.CodeOKYD.ToString();
                NumberTextBox.Text = MainStaffTable.NumDoc.ToString();
            }

            UpdateGrid();
        }

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdditingFlag)
            {
                var staffTableLogic = new StaffTableLogic(LoginFormWindow.connection);
                staffTableLogic.CreateObject(MainStaffTable);

                int pk = staffTableLogic.GetPrimaryKey(MainStaffTable);
                if (pk > 0)
                    MainStaffTable.PrimaryKey = pk;
            }

            AddStaffTableItem addStaffTableItem = new AddStaffTableItem();
            addStaffTableItem.Owner = this;
            addStaffTableItem.StaffTableID = MainStaffTable.PrimaryKey;
            addStaffTableItem.ShowDialog();
            UpdateGrid();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //if (AdditingFlag)
            {
                StringStaffTableViewModel path = StaffTableGrid.SelectedItem as StringStaffTableViewModel;
                var stringStaffTableLogic = new StringStaffTableLogic(LoginFormWindow.connection);
                stringStaffTableLogic.DeleteObject(path.PrimaryKey);
            }
            UpdateGrid();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StaffTableGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //if (MainStaffTable != null)
            {
                StringStaffTableViewModel path = StaffTableGrid.SelectedItem as StringStaffTableViewModel;
                var stringStaffTableLogic = new StringStaffTableLogic(LoginFormWindow.connection);
                stringStaffTableLogic.UpdateObject(path);
            }
            UpdateGrid();
        }

        private void DateCreatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MainStaffTable.CreateDate = (DateTime)DateCreatePicker.SelectedDate;
        }

        private void DateStartPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MainStaffTable.StartDate = (DateTime)DateStartPicker.SelectedDate;
        }

        private void DateEndPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MainStaffTable.EndDate = (DateTime)DateEndPicker.SelectedDate;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
          
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var staffTableLogic = new StaffTableLogic(LoginFormWindow.connection);
            staffTableLogic.UpdateObject(MainStaffTable);
            this.DialogResult = true;
        }

        private void NumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainStaffTable.NumDoc = Convert.ToInt32(NumberTextBox.Text);
        }
    }
}
