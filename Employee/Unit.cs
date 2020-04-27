using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public class Unit
    {
        public int PrimaryKey { get; set; }
        public string Name { get; set; }

        public Unit() { }

        public Unit(int pk, string name) {
            PrimaryKey = pk;
            Name = name;
        }
    }
}
