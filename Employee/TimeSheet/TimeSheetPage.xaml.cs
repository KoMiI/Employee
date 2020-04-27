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

using MySql.Data.MySqlClient;

using UriKind = System.UriKind;

namespace Employee.TimeSheet
{
    /// <summary>
    /// Логика взаимодействия для TimeSheetPagexaml.xaml
    /// </summary>
    public partial class TimeSheetPage : Page
    {
        private ObservableCollection<TimeTrackingViewModel> timeTrackings = new ObservableCollection<TimeTrackingViewModel>();
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

        private void FilterMenuItem_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void PrintMenuItem_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMenuItem_OnClick(object sender, RoutedEventArgs e) {
            var select = DataGrid.SelectedItem;
            if (select is TimeTrackingViewModel viewModel) {
                var timeTrackingLogic = new TimeSheetLogic(MainWindow.connection);
                timeTrackingLogic.DeleteObject(viewModel.TimeTracking.PrimaryKey);
            }
        }
    }
}
