using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.TimeSheet
{
    public class TimeTrackingViewModel : INotifyPropertyChanged
    {
        public readonly string PrimaryKey;

        public string NumberDocument { get; set; }

        public DateTime DateСompilation { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { set; get; }

        public readonly string PKUnit;

        public TimeTrackingViewModel() {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
