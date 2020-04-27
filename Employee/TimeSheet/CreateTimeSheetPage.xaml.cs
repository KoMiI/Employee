using System.Windows;
using System.Windows.Controls;

using Employee.Database;

namespace Employee.TimeSheet
{
    public partial class CreateTimeSheetPage : Page
    {
        private TimeTrackingViewModel viewModel = new TimeTrackingViewModel();
        private TimeSheetLogic timeSheetLogic;
        public CreateTimeSheetPage()
        {
            InitializeComponent();
            this.Loaded += CreateTimeSheetPage_Loaded;
        }

        private void CreateTimeSheetPage_Loaded(object sender, RoutedEventArgs e) {
            timeSheetLogic = new TimeSheetLogic(MainWindow.connection);
            var logic = new UnitLogic(MainWindow.connection);
            var list = logic.GetAll();
            UnitComboBox.ItemsSource = list;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e) {
            viewModel.NumberDocument = NumberDocumentTextBox.Text;
            viewModel.DateСompilation = SostavDatePicker.DisplayDate.ToShortDateString();
            viewModel.BeginDate = FromDatePicker.DisplayDate.ToShortDateString();
            viewModel.EndDate = ToDatePicker.DisplayDate.ToShortDateString();
            viewModel.Unit = UnitComboBox.SelectionBoxItem as Unit;

            var newTracking = viewModel.saveChanged();
            timeSheetLogic.CreateObject(newTracking);
        }
    }
}
