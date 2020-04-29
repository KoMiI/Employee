using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class Result
    {
        public int PrimaryKey;
        public int NotWork { get; set; }
        public int Holydays { get; set; }
        public int NightHours { get; set; }
        public int HoursOverwork { get; set; }
        public int HoursWork { get; set; }
        public int DayWork { get; set; }

        public Result() { }

        public Result(int pk, int notWork, int holydays, int nightHours, int hoursOverwork, int hoursWork, int dayWork) {
            PrimaryKey = pk;
            Holydays = holydays;
            NightHours = nightHours;
            HoursOverwork = hoursOverwork;
            HoursWork = hoursWork;
            DayWork = dayWork;
        }
    }
}
