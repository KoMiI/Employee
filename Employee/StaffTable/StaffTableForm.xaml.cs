using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using Employee.DataBase;
using ExcelObj = Microsoft.Office.Interop.Excel;
using Path = System.IO.Path;

namespace Employee.StaffTable
{
    /// <summary>
    /// Логика взаимодействия для StaffTable.xaml
    /// </summary>
    public partial class StafTable : Window
    {
        public StaffTableViewModel MainStaffTable = null;
        public bool AdditingFlag = false;

        ExcelObj.Application app = new ExcelObj.Application();
        ExcelObj.Workbook workbook;
        ExcelObj.Worksheet NwSheet;

        public StafTable()
        {
            InitializeComponent();
            FillComboBox();
            if (AdditingFlag)
                SaveButton.Content = "Добавить";
            MainStaffTable = new StaffTableViewModel();
        }

        private void UpdateGrid()
        {
            {
                StaffTableGrid.ItemsSource = null;
                var stringStaffTableLogic = new StringStaffTableLogic(MainWindow.connection);
                var stringStaffTable = stringStaffTableLogic.GetAll(MainStaffTable.PrimaryKey);

                MainStaffTable.StaffLines.Clear();

                foreach (var stringStaff in stringStaffTable)
                {
                    MainStaffTable.StaffLines.Add(stringStaff);
                }

                StaffTableGrid.ItemsSource = MainStaffTable.StaffLines;
            }
        }
        public void FillComboBox()
        {
            var orderLogic = new OrderLogic(MainWindow.connection);
            var orders = orderLogic.GetAll();
            OrderComdoBox.ItemsSource = orders;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainStaffTable == null)
                this.Close();
            else
            {
                if (!AdditingFlag)
                {
                    OrderComdoBox.SelectedIndex = MainStaffTable.Order.PrimaryKey-1;
                    DateCreatePicker.SelectedDate = MainStaffTable.CreateDate;
                    DateStartPicker.SelectedDate = MainStaffTable.StartDate;
                    DateEndPicker.SelectedDate = MainStaffTable.EndDate;
                    OKYDTextBox.Text = MainStaffTable.CodeOKYD.ToString();
                    NumberTextBox.Text = MainStaffTable.NumDoc.ToString();
                }

                UpdateGrid();
            }
        }

        private void AddLineButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdditingFlag)
            {
                var staffTableLogic = new StaffTableLogic(MainWindow.connection);
                staffTableLogic.CreateObject(MainStaffTable);

                int pk = staffTableLogic.GetPrimaryKey(MainStaffTable);
                if (pk > 0)
                    MainStaffTable.PrimaryKey = pk;
            }

            AddStaffTableItem addStaffTableItem = new AddStaffTableItem();
            addStaffTableItem.Owner = this;
            addStaffTableItem.StaffTableID = MainStaffTable.PrimaryKey;
            addStaffTableItem.isAdding = true;
            addStaffTableItem.ShowDialog();
            MainStaffTable.StaffLines.Add(addStaffTableItem.MainStringStaffTable);
            UpdateGrid();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            {
                StringStaffTableViewModel path = StaffTableGrid.SelectedItem as StringStaffTableViewModel;
                var stringStaffTableLogic = new StringStaffTableLogic(MainWindow.connection);
                stringStaffTableLogic.DeleteObject(path.PrimaryKey);
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
          this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var staffTableLogic = new StaffTableLogic(MainWindow.connection);
            staffTableLogic.UpdateObject(MainStaffTable);
            this.DialogResult = true;
        }

        private void NumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainStaffTable.NumDoc = Convert.ToInt32(NumberTextBox.Text);
        }

        private void OrderComdoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainStaffTable.Order = (OrderViewModel)OrderComdoBox.SelectedItem;
        }


        private void StaffTableGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StringStaffTableViewModel path = StaffTableGrid.SelectedItem as StringStaffTableViewModel;
            if (path != null)
            {
                AddStaffTableItem addStaffTableItem = new AddStaffTableItem();
                addStaffTableItem.Owner = this;
                addStaffTableItem.StaffTableID = MainStaffTable.PrimaryKey;

                addStaffTableItem.isAdding = false;
                addStaffTableItem.MainStringStaffTable = path;

                addStaffTableItem.SubdivisionComboBox.SelectedIndex = path.Unit.PrimaryKey-1;
                addStaffTableItem.PositionComboBox.SelectedIndex = path.Position.PrimaryKey-1;

                addStaffTableItem.CountTextBox.Text = path.PositionCount.ToString();
                addStaffTableItem.TariffTextBox.Text = path.Tariff.ToString();
                addStaffTableItem.Perks1TextBox.Text = path.Perks.ToString();
                addStaffTableItem.NoteTextBox.Text = path.Note;

                addStaffTableItem.ShowDialog();
                if(addStaffTableItem.DialogResult == true)
                    UpdateGrid();
            }
        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            // Файл шаблона
            const string template = "shtatnoe-raspisanie.xls";

            // Открываем книгу
            workbook = app.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, template));
            // Получаем активную таблицу
            NwSheet = workbook.ActiveSheet as ExcelObj.Worksheet;

            // Шапка документа
            NwSheet.Range["A4"].Value = MainStaffTable.Organization;
            NwSheet.Range["J8"].Value = MainStaffTable.NumDoc.ToString();
            NwSheet.Range["R8"].Value = MainStaffTable.CreateDate.ToString();
            NwSheet.Range["I12"].Value = MainStaffTable.StartDate.Day.ToString();
            NwSheet.Range["M12"].Value = MainStaffTable.StartDate.Month.ToString();
            NwSheet.Range["S12"].Value = MainStaffTable.StartDate.Year.ToString();

            NwSheet.Range["AH10"].Value = MainStaffTable.Order.DocDate.Day.ToString();
            NwSheet.Range["AK10"].Value = MainStaffTable.Order.DocDate.Month.ToString();
            NwSheet.Range["AT10"].Value = MainStaffTable.Order.DocDate.Year.ToString();
            NwSheet.Range["AX10"].Value = MainStaffTable.Order.NumDoc;

            int allCount = 0;
            double allTariff = 0, allPerks = 0;
            foreach (var staffLine in MainStaffTable.StaffLines)
            {
                allCount += staffLine.PositionCount;
                allTariff += staffLine.Tariff;
                allPerks += staffLine.Perks;
            }
            NwSheet.Range["AE12"].Value = allCount.ToString();

            // Строки таблицы
            for (int i = 0; i < MainStaffTable.StaffLines.Count; i++)
            {
                NwSheet.Cells[17 + i, "A"].Value = MainStaffTable.StaffLines[i].Unit.Name;
                NwSheet.Cells[17 + i, "D"].Value = MainStaffTable.StaffLines[i].Unit.PrimaryKey.ToString();
                NwSheet.Cells[17 + i, "F"].Value = MainStaffTable.StaffLines[i].Position.Name;
                NwSheet.Cells[17 + i, "P"].Value = MainStaffTable.StaffLines[i].PositionCount.ToString();
                NwSheet.Cells[17 + i, "T"].Value = MainStaffTable.StaffLines[i].Tariff.ToString();
                NwSheet.Cells[17 + i, "AI"].Value = MainStaffTable.StaffLines[i].Perks.ToString();
                NwSheet.Cells[17 + i, "AM"].Value = MainStaffTable.StaffLines[i].Note;
            }
            NwSheet.Range["P25"].Value = allCount.ToString();
            NwSheet.Range["T25"].Value = allTariff.ToString();
            NwSheet.Range["AI25"].Value = allPerks.ToString();
            
            
            //TopMost = true;

            string savedFileName = "StaffTable.xls";
            workbook.SaveAs(Path.Combine(Environment.CurrentDirectory, savedFileName));
            CloseExcel();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);

        public void CloseExcel()
        {
            if (app != null)
            {
                int excelProcessId = -1;
                GetWindowThreadProcessId(app.Hwnd, ref excelProcessId);

                Marshal.ReleaseComObject(NwSheet);
                workbook.Close();
                Marshal.ReleaseComObject(workbook);
                app.Quit();
                Marshal.ReleaseComObject(app);

                app = null;
                // Прибиваем висящий процесс
                try
                {
                    Process process = Process.GetProcessById(excelProcessId);
                    process.Kill();
                }
                finally
                {
                }
            }
        }

    }
}
