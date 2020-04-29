using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class Fact
    {
        public int PrimaryKey { get; set; }
        public string Reason { get; set; }
        public int CountDay { get; set; }

        public Fact() { }
        public Fact(int pk, string reason, int count) {
            PrimaryKey = pk;
            Reason = reason;
            CountDay = count;
        }
    }
}
