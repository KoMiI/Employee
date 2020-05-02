using Employee.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.StaffTable
{
    public class StaffTableViewModel
    {
        public string Organization = "КГБУЗ \"Городская больница №1\"";
        public int PrimaryKey { get; set; }
        public OrderViewModel Order { get; set; }

        public int CodeOKYD = 0301017;
        public int NumDoc { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<StringStaffTableViewModel> StaffLines;

        public StaffTableViewModel(int _primaryKey, OrderViewModel _order, int _numDoc, DateTime _createDate, DateTime _startDate, DateTime _endDate)
        {
            PrimaryKey = _primaryKey;
            Order = _order;
            NumDoc = _numDoc;
            CreateDate = _createDate;
            StartDate = _startDate;
            EndDate = _endDate;

            StaffLines = new List<StringStaffTableViewModel>();
        }

        public StaffTableViewModel()
        {
            StaffLines = new List<StringStaffTableViewModel>();
        }
        public void loadData()
        {
            var logic = new StringStaffTableLogic(MainWindow.connection);
            StaffLines = logic.GetAll(PrimaryKey);
        }

    }
}
