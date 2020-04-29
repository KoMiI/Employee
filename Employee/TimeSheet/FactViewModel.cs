using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Employee.Database;

namespace Employee.TimeSheet
{
    public class FactViewModel
    {
        public Fact Fact { get; set; }
        public string Presence { get; set; }
        public int CountDay { get; set; }

        public FactViewModel() { }

        public FactViewModel(Fact model) {
            Fact = model;
            Presence = model.Reason;
            CountDay = model.CountDay;
        }

        public Fact saveChanged(bool isEdit) {
            var newObject = new Fact {CountDay = CountDay, Reason = Presence};

            var factLogic = new FactLogic(MainWindow.connection);
            if (isEdit) {
                newObject.PrimaryKey = Fact.PrimaryKey;
                factLogic.UpdateObject(newObject);
            } else {
                newObject.PrimaryKey = (int)factLogic.CreateObject(newObject);
            }

            return newObject;
        }
    }
}
