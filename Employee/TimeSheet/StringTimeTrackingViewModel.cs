using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class StringTimeTrackingViewModel
    {
        public readonly string PrimaryKey;

        public readonly string PKTimeTracking;

        public readonly string PKPersonalCard;

        public readonly string PKResult;

        public readonly string PKDay; // wtf ???

        public int DurationWork { get; set; }

        public StringTimeTrackingViewModel() { }

    }
}
