using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public class OrderViewModel
    {
        public int PrimaryKey { get; set; }
        public string NumDoc { get; set; }
        public DateTime DocDate { get; set; }
        public OrderType Type { get; set; }

        public OrderViewModel(int _primaryKey, string _docNum, DateTime _docDate, OrderType _type)
        {
            PrimaryKey = _primaryKey;
            NumDoc = _docNum;
            DocDate = _docDate;
            Type = _type;
        }

        public override string ToString()
        {
            return NumDoc;
        }
    }

    public class OrderType
    {
        public int PrimaryKey { get; set; }
        public string Name { get; set; }
        public OrderType(int _primaryKey, string _name)
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
