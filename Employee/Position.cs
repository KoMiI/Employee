using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public class Position
    {
        public int PrimaryKey { get; set; }
        public string Name { get; set; }
        public Position(int _primarykey, string _name)
        {
            PrimaryKey = _primarykey;
            Name = _name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
