using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Employee.Database;

namespace Employee.TimeSheet
{
    public class DayViewModel
    {
        public int PrimaryKey { get; set; }
        public string DayType { get; set; }
        public int DurationWork { get; set; }
        public int StringPrimaryKey { get; set; }
        public string Date { get; set; }

        public DayViewModel() { }

        public DayViewModel(Day model) {
            PrimaryKey = model.PrimaryKey;
            DayType = model.DayType;
            DurationWork = model.DurationWork;
            StringPrimaryKey = model.StringPrimaryKey;
            Date = model.Date.ToShortDateString();
        }

        public Day saveChanged(bool isEdit) {
            var newObject = new Day {
                PrimaryKey = PrimaryKey, DayType = DayType, DurationWork = DurationWork,
                Date = Convert.ToDateTime(Date), StringPrimaryKey = StringPrimaryKey
            };

            var dayLogic = new DayLogic(MainWindow.connection);
            if (isEdit)
                dayLogic.UpdateObject(newObject);
            else
                newObject.PrimaryKey = (int)dayLogic.CreateObject(newObject);

            return newObject;

        }
    }
}
