using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

using Employee.Database;
using Employee.DataBase;

using MySql.Data.MySqlClient;

namespace Employee.TimeSheet
{
    public partial class CreateTimeSheetPage : Page
    {
        public TimeTrackingViewModel ViewModel { get; set; }

        private ObservableCollection<StringTimeTrackingViewModel> strings { get; set; } =
            new ObservableCollection<StringTimeTrackingViewModel>();

        public List<PersonalCard> personalCard { get; set; }
        private TimeSheetLogic timeSheetLogic;

        public bool IsEdit = false;

        public CreateTimeSheetPage() {
            InitializeComponent();
            this.Loaded += CreateTimeSheetPage_Loaded;
            GetAllPersonalCard();
        }

        private void CreateTimeSheetPage_Loaded(object sender, RoutedEventArgs e) {
            timeSheetLogic = new TimeSheetLogic(MainWindow.connection);
            loadData();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e) {
            if (IsEdit)
                updateObject();
            else
                saveObject();
        }

        private void loadData() {
            var logic = new UnitLogic(MainWindow.connection);
            var list = logic.GetAll();
            UnitComboBox.ItemsSource = list;
            if (IsEdit) {
                NumberDocumentTextBox.Text = ViewModel.NumberDocument;
                SostavDatePicker.SelectedDate = ViewModel.TimeTracking.DateСompilation;
                FromDatePicker.SelectedDate = ViewModel.TimeTracking.BeginDate;
                ToDatePicker.SelectedDate = ViewModel.TimeTracking.EndDate;
                UnitComboBox.SelectedItem = list.FirstOrDefault(x => x.PrimaryKey == ViewModel.Unit.PrimaryKey);

                var stringTimeLogic = new StringTimeTrackingLogic(MainWindow.connection);
                var timeStrings = stringTimeLogic.GetObjectByTimeTracking(ViewModel.TimeTracking.PrimaryKey);
                foreach (var stringTimeTracking in timeStrings) {
                    strings.Add(new StringTimeTrackingViewModel(stringTimeTracking));
                }


            } else {
                ViewModel = new TimeTrackingViewModel();
            }

            DataGrid.ItemsSource = strings;

        }

        private void saveObject() {
            ViewModel.NumberDocument = NumberDocumentTextBox.Text;
            ViewModel.DateСompilation = SostavDatePicker.DisplayDate.ToShortDateString();
            ViewModel.BeginDate = FromDatePicker.DisplayDate.ToShortDateString();
            ViewModel.EndDate = ToDatePicker.DisplayDate.ToShortDateString();
            ViewModel.Unit = UnitComboBox.SelectedItem as Unit;

            var newTracking = ViewModel.saveChanged();
            newTracking.PrimaryKey = (int) timeSheetLogic.CreateObject(newTracking);
            ViewModel.TimeTracking = newTracking;
            foreach (var stringTime in strings) {
                stringTime.TimeTracking = ViewModel;
                stringTime.SaveChanged();
            }

            this.NavigationService.GoBack();
        }

        private void updateObject() {
            ViewModel.NumberDocument = NumberDocumentTextBox.Text;
            ViewModel.DateСompilation = SostavDatePicker.DisplayDate.ToShortDateString();
            ViewModel.BeginDate = FromDatePicker.DisplayDate.ToShortDateString();
            ViewModel.EndDate = ToDatePicker.DisplayDate.ToShortDateString();
            ViewModel.Unit = UnitComboBox.SelectedItem as Unit;

            var newTracking = ViewModel.saveChanged();
            timeSheetLogic.UpdateObject(newTracking);
            foreach (var item in DataGrid.Items) {
                var stringTime = item as StringTimeTrackingViewModel;
                stringTime.TimeTracking = ViewModel;
                stringTime.SaveChanged(true);
            }

            this.NavigationService.GoBack();
        }

        private void GetAllPersonalCard()
        {
            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();

            // Сочетать Command с Connection.
            cmd.Connection = MainWindow.connection;
            cmd.CommandText = "SELECT  `PersonalCard`.`pk_personal_card`, `PersonalCard`.`familiya`, `PersonalCard`.`imya`,`PersonalCard`.`tabel_number`, " +
                              "`PersonalCardPriem`.`position`, `PersonalCard`.`otchestvo` FROM `PersonalCard`, `PersonalCardPriem` " +
                              $"WHERE `PersonalCardPriem`.`pk_personal_card` = `PersonalCard`.`pk_personal_card`";
            personalCard = new List<PersonalCard>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        string familiya = Convert.ToString(reader.GetValue(reader.GetOrdinal("familiya")));
                        string name = Convert.ToString(reader.GetValue(reader.GetOrdinal("imya")));
                        int tableNumber = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("tabel_number")));
                        string otchestvo = Convert.ToString(reader.GetValue(reader.GetOrdinal("otchestvo")));
                        string Position = Convert.ToString(reader.GetValue(reader.GetOrdinal("position")));
                        int pk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pk_personal_card")));

                        personalCard.Add(new PersonalCard(pk, familiya, name, otchestvo, Position, tableNumber));
                    }
                }
            }
        }

        private void DeleteStringItem_OnClick(object sender, RoutedEventArgs e) {
            var select = DataGrid.SelectedItem;
            if (select is StringTimeTrackingViewModel viewModel) {
                strings.Remove(viewModel);
                if (IsEdit) {
                    var timeTrackingLogic = new StringTimeTrackingLogic(MainWindow.connection);
                    timeTrackingLogic.DeleteObject(viewModel.StringTimeTracking.PrimaryKey);
                }
            }
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e) {
            var select = DataGrid.SelectedItem;
            if (select is DayViewModel viewModel) {
                var stringItem = strings.FirstOrDefault(x => x.StringTimeTracking.PrimaryKey == viewModel.StringPrimaryKey);
                var item = stringItem.Days.FirstOrDefault(x => x.PrimaryKey == viewModel.PrimaryKey);
                stringItem.Days.Remove(item);
                if (IsEdit) {

                    var dayLogic = new DayLogic(MainWindow.connection);
                    dayLogic.DeleteObject(viewModel.PrimaryKey);
                }
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e) {
            if (sender is ComboBox comboBox) {
                comboBox.ItemsSource = personalCard;
            }
        }

        private void FioComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                var item = strings[DataGrid.SelectedIndex];
                if (item != null)
                    item.PersonalCard = (sender as ComboBox).SelectedItem as PersonalCard;
            }
            catch (Exception exception) {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
