using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class Day
    {
        public int PrimaryKey { get; set; }
        public string DayType { get; set; }
        public int DurationWork { get; set; }
        public int StringPrimaryKey { get; set; }
        public DateTime Date { get; set; }

        public Day() { }
        public Day(int pk, string dayType, int duration, int strPk, DateTime date) {
            PrimaryKey = pk;
            DayType = dayType;
            DurationWork = duration;
            StringPrimaryKey = strPk;
            Date = date;
        }
    }

    public class CompareDay : IComparer<Day>
    {
        public int Compare(Day x, Day y) {
            if (x.Date > y.Date)
                return 1;
            else if (x.Date < y.Date)
                return 0;
            else return -1;
        }
    }
}
