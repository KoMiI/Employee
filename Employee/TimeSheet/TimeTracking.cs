using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class TimeTracking
    {
        public int PrimaryKey;

        public string NumberDocument { get; set; }

        public DateTime DateСompilation { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { set; get; }

        public int PKUnit;

        public TimeTracking() { }
        public TimeTracking(int PK, string numberDocument, DateTime date, DateTime from, DateTime to, int PK_unit) {
            PrimaryKey = PK;
            NumberDocument = numberDocument;
            DateСompilation = date;
            BeginDate = from;
            EndDate = to;
            PKUnit = PK_unit;
        }
    }
}
