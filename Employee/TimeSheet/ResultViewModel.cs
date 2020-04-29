using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Employee.Database;

namespace Employee.TimeSheet
{
    public class ResultViewModel
    {
        public Result Result { get; set; }
        public int NotWork { get; set; }
        public int Holydays { get; set; }
        public int NightHours { get; set; }
        public int HoursOverwork { get; set; }
        public int HoursWork { get; set; }
        public int DayWork { get; set; }

        public ResultViewModel() { }

        public ResultViewModel(Result model) {
            Result = model;
            NotWork = model.NotWork;
            Holydays = model.Holydays;
            NightHours = model.NightHours;
            HoursOverwork = model.HoursOverwork;
            HoursWork = model.HoursWork;
            DayWork = model.DayWork;
        }

        public Result saveChanged(bool isEdit) {
            var newObject = new Result {
                DayWork = DayWork, Holydays = Holydays, HoursOverwork = HoursOverwork, HoursWork = HoursWork,
                NightHours = NightHours, NotWork = NotWork
            };

            var logic = new ResultLogic(MainWindow.connection);
            if (isEdit) {
                newObject.PrimaryKey = Result.PrimaryKey;
                logic.UpdateObject(newObject);
            } else {
                newObject.PrimaryKey = (int) logic.CreateObject(newObject);
            }

            return newObject;
        }
    }
}
