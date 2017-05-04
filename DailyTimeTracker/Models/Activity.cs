using System;
using GalaSoft.MvvmLight;
using Humanizer;

namespace DailyTimeTracker.Models {
    public class Activity : ObservableObject
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ActivityCategory Category { get; set; }
        public string Description { get; set; }
        public string Day => StartTime.ToShortDateString() + " " + StartTime.DayOfWeek;

        public string Duration => EndTime == null ? "Ongoing" : (EndTime.Value - StartTime).Humanize();

        public override string ToString() => $"ID:{Id}\nStart Time:{StartTime.ToShortTimeString()}\nEnd Time:{EndTime?.ToShortTimeString()}\nCategory:{Category}\nDescription:{Description}\nDay:{Day}\nDuration:{Duration}"; 
    }
}
