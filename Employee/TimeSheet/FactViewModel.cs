using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class FactViewModel
    {
        public readonly string PrimaryKey;

        public readonly string PKTimeTracking;

        public readonly string PKStringTimeTrackingViewModel;
        public DateTime Data { get; set; }

        public int Presence { get; set; }

        public string Cause { get; set; }

        public  FactViewModel() { }

    }
}
