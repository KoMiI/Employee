using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class StringTimeTracking
    {
        public int PrimaryKey { get; set; }
        public int PKTimeTracking { get; set; }

        public int PKPersonalCard { get; set; }

        public int PKResult { get; set; }

        public int PKFact { get; set; }

        public StringTimeTracking() { }

        public StringTimeTracking(int pk, int pktime, int pkpersonal, int pkResult, int pkFact) {
            PrimaryKey = pk;
            PKTimeTracking = pktime;
            PKPersonalCard = pkpersonal;
            PKResult = pkResult;
            PKFact = pkFact;
        }

    }
}
