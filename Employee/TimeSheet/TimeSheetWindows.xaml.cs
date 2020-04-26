using System;
using System.Collections.ObjectModel;
using System.Windows;

using Employee.Database;

namespace Employee.TimeSheet
{
    /// <summary>
    /// Логика взаимодействия для TimeSheetWindows.xaml
    /// </summary>
    public partial class TimeSheetWindows : Window
    {

        private ObservableCollection<TimeTracking> timeTrackings = new ObservableCollection<TimeTracking>();
 
        public TimeSheetWindows()
        {
            InitializeComponent();
            var logic = new TimeSheetLogic(MainWindow.connection);
            var list = logic.GetAll();
            foreach (var timeTracking in list) {
                timeTrackings.Add(timeTracking);
            }

            DataGrid.ItemsSource = timeTrackings;
        }

        private void loadData() {

        }

        private void AddTabelMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddStringMenuItem_OnClick(object sender, RoutedEventArgs e) {
            
        }

        private void FilterMenuItem_OnClick(object sender, RoutedEventArgs e) {
            
        }

        private void PrintMenuItem_OnClick(object sender, RoutedEventArgs e) {
            
        }
    }
}
