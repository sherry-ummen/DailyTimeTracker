using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Humanizer;

namespace DailyTimeTracker.Models
{
    public class Activity : ObservableObject
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ActivityCategory Category { get; set; }
        public string Description { get; set; }
        public string Day { get { return StartTime.ToShortDateString() + " " + StartTime.DayOfWeek.ToString(); } }

        public string Duration {
            get {
                return EndTime == null ? "Still counting" : (EndTime.Value - StartTime).Humanize();
            }
        }
    }
}
