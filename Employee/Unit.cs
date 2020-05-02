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

        public Unit(int _primaryKey, string _name)
        {
            PrimaryKey = _primaryKey;
            Name = _name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
