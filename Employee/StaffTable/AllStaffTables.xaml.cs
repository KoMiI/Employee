﻿using System;
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
using System.Windows.Shapes;
using Employee.DataBase;

namespace Employee.StaffTable
{
    /// <summary>
    /// Логика взаимодействия для AllStaffTables.xaml
    /// </summary>
    public partial class AllStaffTables : Page
    {
        private ObservableCollection<StaffTableViewModel> _staffTables = new ObservableCollection<StaffTableViewModel>();

        public AllStaffTables()
        {
            InitializeComponent();
        }

        private void UpdateGrid()
        {
            AllStaffTablesDataGrid.ItemsSource = null;
            var staffTableLogic = new StaffTableLogic(MainWindow.connection);
            var staffTableist = staffTableLogic.GetAll();

            _staffTables.Clear();

            foreach (var staffTable in staffTableist) 
            {
                _staffTables.Add(staffTable);
            }

            AllStaffTablesDataGrid.ItemsSource = _staffTables;
        }


        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            StaffTableViewModel path = AllStaffTablesDataGrid.SelectedItem as StaffTableViewModel;
            var staffTableLogic = new StaffTableLogic(MainWindow.connection);
            staffTableLogic.DeleteObject(path.PrimaryKey);

            UpdateGrid();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            StaffTable.StafTable _StaffTable = new StaffTable.StafTable();

            var staffTableLogic = new StaffTableLogic(MainWindow.connection);

            _StaffTable.AdditingFlag = true;
            _StaffTable.ShowDialog();
            if (_StaffTable.DialogResult == true)
            {
                _staffTables.Add(_StaffTable.MainStaffTable);
                staffTableLogic.CreateObject(_StaffTable.MainStaffTable);
                UpdateGrid();
            }
        }

        private void AllStaffTablesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StaffTable.StafTable _StaffTable = new StaffTable.StafTable();
            StaffTableViewModel path = AllStaffTablesDataGrid.SelectedItem as StaffTableViewModel;
            _StaffTable.MainStaffTable = path;
            _StaffTable.AdditingFlag = false;
            _StaffTable.ShowDialog();

            if (_StaffTable.DialogResult == true)
            {
                var staffTableLogic = new StaffTableLogic(MainWindow.connection);
                staffTableLogic.UpdateObject(_StaffTable.MainStaffTable);
                UpdateGrid();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }
    }
}
