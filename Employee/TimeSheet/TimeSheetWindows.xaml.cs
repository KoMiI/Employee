using System.Windows;

namespace Employee.TimeSheet
{
    /// <summary>
    /// Логика взаимодействия для TimeSheetWindows.xaml
    /// </summary>
    public partial class TimeSheetWindows : Window
    {
        private TimeTrackingViewModel timeTrackingViewModel;

        public TimeSheetWindows()
        {
            InitializeComponent();
        }

        private void loadData() {

        }

        private void AddTabelMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddStringMenuItem_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void FilterMenuItem_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }

        private void PrintMenuItem_OnClick(object sender, RoutedEventArgs e) {
            throw new System.NotImplementedException();
        }
    }
}
