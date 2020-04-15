using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class DayViewModel
    {
        public readonly string PrimaryKey;

        public string DayType { get; set; }

        public int DurationWork { get; set; }
    }
}
