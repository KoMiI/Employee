using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.StaffTable
{
    public class StaffingTable
    {
        public int PrimaryKey { get; }

        public int CodeOKYD = 0301017;
        public int CodeOKPO { get; set; }
        public int NumDoc { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StaffCount { get; set; }
        public double SumTariff { get; set; }
        public List<StringStaffTableViewModel> StaffLines;
        public StaffingTable()
        {
            StaffLines = new List<StringStaffTableViewModel>();
        }
        public StaffingTable(int _primaryKey, int _numDoc, DateTime _createDate, DateTime _startDate, DateTime _endDate)
        {
            PrimaryKey = _primaryKey;
            NumDoc = _numDoc;
            CreateDate = _createDate;
            StartDate = _startDate;
            EndDate = _endDate;

            StaffLines = new List<StringStaffTableViewModel>();
        }
    }
}
