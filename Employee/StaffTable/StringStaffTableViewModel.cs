using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.StaffTable
{
    public class StringStaffTableViewModel
    {
        public int PrimaryKey { get; set; }
        public Unit Unit { get; set; }
        public Position Position { get; set; }
        public int PositionCount { get; set; }
        public double Tariff { get; set; }
        public double Perks { get; set; }
        public string Note { get; set; }
        public int StaffingTableKey { get; set; }

        public StringStaffTableViewModel() { }

        public StringStaffTableViewModel(int _primaryKey, int _positionCount, double _tariff, double _perks, string _note,
            Unit _subdivision, Position _position, int _staffingTableKey)
        {
            PrimaryKey = _primaryKey;
            PositionCount = _positionCount;
            Tariff = _tariff;
            Perks = _perks;
            Note = _note;
            Unit = _subdivision;
            Position = _position;
            StaffingTableKey = _staffingTableKey;
        }

    }
}
