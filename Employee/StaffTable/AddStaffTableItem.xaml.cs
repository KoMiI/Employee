using System;
using System.Windows;
using System.Windows.Controls;
using Employee.DataBase;


namespace Employee.StaffTable
{
    /// <summary>
    /// Логика взаимодействия для AddStaffTableItem.xaml
    /// </summary>
    public partial class AddStaffTableItem : Window
    {
        private Unit _unit = null;
        private Position _position = null;

        public StringStaffTableViewModel MainStringStaffTable = null;
        public int StaffTableID;
        public bool AdditingFlag = false;
        public AddStaffTableItem()
        {
            InitializeComponent();
            FillComboBox();
            MainStringStaffTable = new StringStaffTableViewModel();
        }
        public void FillComboBox()
        {
            var unitLogic = new UnitLogic(MainWindow.connection);
            var units = unitLogic.GetAll();
            SubdivisionComboBox.ItemsSource = units;

            var positionLogic = new PositionLogic(MainWindow.connection);
            var positions = positionLogic.GetAll();
            PositionComboBox.ItemsSource = positions;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (AdditingFlag)
                AddButton.Content = "Добавить";
            else
            {
                AddButton.Content = "Сохранить";
            }
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
            MainStringStaffTable.Unit = _unit;
            MainStringStaffTable.Position = _position;
            MainStringStaffTable.PositionCount = Convert.ToInt32(CountTextBox.Text);
            MainStringStaffTable.Tariff = Convert.ToDouble(TariffTextBox.Text);
            MainStringStaffTable.Perks = Convert.ToDouble(Perks1TextBox.Text);
            MainStringStaffTable.Note = NoteTextBox.Text;
            MainStringStaffTable.StaffingTableKey = StaffTableID;

            if (AdditingFlag)
            {
                var stringStaffTableLogic = new StringStaffTableLogic(MainWindow.connection);
                stringStaffTableLogic.CreateObject(MainStringStaffTable);

                int pk = stringStaffTableLogic.GetPrimaryKey(MainStringStaffTable);
                if (pk > 0)
                {
                    MainStringStaffTable.PrimaryKey = pk;
                    stringStaffTableLogic.UpdateObject(MainStringStaffTable);
                }
            }
            else
            {
                var stringStaffTableLogic = new StringStaffTableLogic(MainWindow.connection);
                stringStaffTableLogic.UpdateObject(MainStringStaffTable);

            }

            this.DialogResult = true;
        }
    }
}
