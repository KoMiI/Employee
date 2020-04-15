using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class ResultViewModel
    {
        public readonly string PimaryKey;

        public int NotWork { get; set; }

        public int Holydays { get; set; }

        public int NightHours { get; set; }

        public int HoursOverwork { get; set; }

        public int HoursWork { get; set; }

        public int DayWork { get; set; }

        public ResultViewModel() { }

    }
}
