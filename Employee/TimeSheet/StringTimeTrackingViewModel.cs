using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Employee.Database;

namespace Employee.TimeSheet
{
    public class StringTimeTrackingViewModel
    {
        public StringTimeTracking StringTimeTracking { get; set; }

        public TimeTrackingViewModel TimeTracking { get; set; }

        public PersonalCard PersonalCard { get; set; }

        public ResultViewModel Result { get; set; }

        public FactViewModel Fact { get; set; } 

        public ObservableCollection<DayViewModel> Days { get; set; } = new ObservableCollection<DayViewModel>();

        public StringTimeTrackingViewModel() {
            Result = new ResultViewModel();
            Fact = new FactViewModel();
        }

        public StringTimeTrackingViewModel(StringTimeTracking model) {
            StringTimeTracking = model;
            loadDate();
        }

        private void loadDate() {
            var resultLogic = new ResultLogic(MainWindow.connection);
            Result = new ResultViewModel(resultLogic.GetObject(StringTimeTracking.PKResult));

            var factLogic = new FactLogic(MainWindow.connection);
            Fact = new FactViewModel(factLogic.GetObject(StringTimeTracking.PKFact));

            var dayLogic = new DayLogic(MainWindow.connection);
            var days = dayLogic.GetObjectByTimeTracking(StringTimeTracking.PrimaryKey);
            days.Sort(new CompareDay());
            foreach (var day in days) {
                Days.Add(new DayViewModel(day));
            }

            PersonalCard = new PersonalCard(StringTimeTracking.PKPersonalCard);
        }

        public StringTimeTracking SaveChanged(bool isEdit = false) {
            var newObject = new StringTimeTracking();
            newObject.PKFact = Fact.saveChanged(isEdit).PrimaryKey;
            newObject.PKPersonalCard = PersonalCard.PrimaryKey;
            newObject.PKResult = Result.saveChanged(isEdit).PrimaryKey;
            newObject.PKTimeTracking = TimeTracking.TimeTracking.PrimaryKey;

            var logic = new StringTimeTrackingLogic(MainWindow.connection);
            if (isEdit) {
                newObject.PrimaryKey = StringTimeTracking.PrimaryKey;
                logic.UpdateObject(newObject);
            } else {
                logic.CreateObject(newObject);
            }
            return newObject;
        }

    }
}
