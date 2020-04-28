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
    /// Логика взаимодействия для AddStaffTableItem.xaml
    /// </summary>
    public partial class AddStaffTableItem : Window
    {
        private Unit _unit = null;
        private Position _position = null;
        public StafTable main;
        public int StaffTableID;
        public AddStaffTableItem()
        {
            main = Owner as StafTable;
            InitializeComponent();
            FillComboBox();
        }
        public void FillComboBox()
        {
            var unitLogic = new UnitLogic(LoginFormWindow.connection);
            var units = unitLogic.GetAll();
            SubdivisionComboBox.ItemsSource = units;

            var positionLogic = new PositionLogic(LoginFormWindow.connection);
            var positions = positionLogic.GetAll();
            PositionComboBox.ItemsSource = positions;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SubdivisionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _unit = (Unit)SubdivisionComboBox.SelectedItem;

        }

        private void PositionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _position = (Position)PositionComboBox.SelectedItem;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            StringStaffTableViewModel tempStringStaffTable = new StringStaffTableViewModel();

                tempStringStaffTable.Unit = _unit;
                tempStringStaffTable.Position = _position;
                tempStringStaffTable.PositionCount = Convert.ToInt32(CountTextBox.Text);
                tempStringStaffTable.Tariff = Convert.ToDouble(TariffTextBox.Text);
                tempStringStaffTable.Perks = Convert.ToDouble(Perks1TextBox.Text) +
                                             Convert.ToDouble(Perks2TextBox.Text) +
                                             Convert.ToDouble(Perks3TextBox.Text);
                tempStringStaffTable.Note = NoteTextBox.Text;
                tempStringStaffTable.StaffingTableKey = StaffTableID;

                //if (main != null)
                //    main.MainStaffTable.StaffLines.Add(tempStringStaffTable);


                var stringStaffTableLogic = new StringStaffTableLogic(LoginFormWindow.connection);
                stringStaffTableLogic.CreateObject(tempStringStaffTable);
                int pk = stringStaffTableLogic.GetPrimaryKey(tempStringStaffTable);
                if (pk > 0)
                {
                    tempStringStaffTable.PrimaryKey = pk;
                    stringStaffTableLogic.UpdateObject(tempStringStaffTable);
            }

                this.DialogResult = true;
        }
    }
}
