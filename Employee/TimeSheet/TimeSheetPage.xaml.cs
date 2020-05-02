using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Employee.Database;
using Employee.DataBase;

using MySql.Data.MySqlClient;

using UriKind = System.UriKind;

namespace Employee.TimeSheet
{
    /// <summary>
    /// Логика взаимодействия для TimeSheetPagexaml.xaml
    /// </summary>
    public partial class TimeSheetPage : Page
    {
        private ObservableCollection<TimeTrackingViewModel> timeTrackings { get; set; } = new ObservableCollection<TimeTrackingViewModel>();
        public TimeSheetPage()
        {
            InitializeComponent();
            this.Loaded += TimeSheetPage_Loaded;

            
        }

        private void TimeSheetPage_Loaded(object sender, RoutedEventArgs e) {
            loadData();
        }

        private void loadData() {
            var timeTrackingLogic = new TimeSheetLogic(MainWindow.connection);
            var timeTrackingList = timeTrackingLogic.GetAll();
            foreach (var timeTracking in timeTrackingList)
            {
                timeTrackings.Add(new TimeTrackingViewModel(timeTracking));
            }

            DataGrid.ItemsSource = timeTrackings;

            var unitLogic = new UnitLogic(MainWindow.connection);
            var units = unitLogic.GetAll();
            UnitComboBox.ItemsSource = units;
        }


        private void AddTabelMenuItem_Click(object sender, RoutedEventArgs e) {
            Uri uri = new Uri("TimeSheet/CreateTimeSheetPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void DeleteMenuItem_OnClick(object sender, RoutedEventArgs e) {
            var select = DataGrid.SelectedItem;
            if (select is TimeTrackingViewModel viewModel) {
                timeTrackings.Remove(viewModel);
                var timeTrackingLogic = new TimeSheetLogic(MainWindow.connection);
                timeTrackingLogic.DeleteObject(viewModel.TimeTracking.PrimaryKey);
            }
        }

        private void EditMenuItem_OnClick(object sender, RoutedEventArgs e) {
            var select = DataGrid.SelectedItem;
            if (select is TimeTrackingViewModel viewModel) {
                var createPage = new CreateTimeSheetPage();
                createPage.ViewModel = viewModel;
                createPage.IsEdit = true;
                this.NavigationService.Navigate(createPage);
            }

        }

        private void UnitComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            find();
        }

        private void DatePickerFrom_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            find();
        }


        private void find() {
            var resultFind = timeTrackings.Where(x => x.Unit.PrimaryKey == ((Unit)UnitComboBox.SelectedItem).PrimaryKey);
            if (DatePickerFrom.SelectedDate != null) {
                resultFind = resultFind.Where(x => x.TimeTracking.BeginDate >= DatePickerFrom.SelectedDate);
            }
            if (DatePickerTo.SelectedDate != null) {
                resultFind = resultFind.Where(x => x.TimeTracking.EndDate >= DatePickerTo.SelectedDate);
            }

            DataGrid.ItemsSource = resultFind;
        }


    }
}
