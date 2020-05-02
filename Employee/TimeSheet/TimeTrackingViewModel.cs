using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Employee.Database;
using Employee.DataBase;

namespace Employee.TimeSheet
{
    public class TimeTrackingViewModel 
    {
        public TimeTracking TimeTracking;
        public string NumberDocument { get; set; }

        public string DateСompilation { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { set; get; }

        public Unit Unit { get; set; }

        public TimeTrackingViewModel() { }

        public TimeTrackingViewModel(TimeTracking logicModel) {
            TimeTracking = logicModel;
            NumberDocument = logicModel.NumberDocument;
            DateСompilation = logicModel.DateСompilation.ToShortDateString();
            BeginDate = logicModel.BeginDate.ToShortDateString();
            EndDate = logicModel.EndDate.ToShortDateString();
            loadData();
        }

        public void loadData() {
            var logic = new UnitLogic(MainWindow.connection);
            Unit = logic.GetObject(TimeTracking.PKUnit);
        }

        public TimeTracking saveChanged() {
            TimeTracking timeTracking = new TimeTracking();
            timeTracking.NumberDocument = NumberDocument;
            DateTime.TryParse(DateСompilation, out DateTime date);
            timeTracking.DateСompilation = date;
            DateTime.TryParse(BeginDate, out DateTime from);
            timeTracking.BeginDate = from;
            DateTime.TryParse(EndDate, out DateTime to);
            timeTracking.EndDate = to;
            timeTracking.PKUnit = Unit.PrimaryKey;

            return timeTracking;
        }
    }
}
