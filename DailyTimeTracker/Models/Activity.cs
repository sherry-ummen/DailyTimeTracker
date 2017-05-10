using System;
using GalaSoft.MvvmLight;
using Humanizer;

namespace DailyTimeTracker.Models {
    public class Activity : ObservableObject {
        public int Id { get; set; }

        private DateTime _startTime;
        public DateTime StartTime {
            get {
                return _startTime;
            }
            set {
                Set(() => StartTime, ref _startTime, value);
                RaisePropertyChanged(() => Duration);
            }
        }

        private DateTime? _endTime;
        public DateTime? EndTime {
            get {
                return _endTime;
            }
            set {
                Set(() => EndTime, ref _endTime, value);
                RaisePropertyChanged(() => Duration);
            }
        }

        private ActivityCategory _category;
        public ActivityCategory Category {
            get {
                return _category;
            }
            set {
                Set(() => Category, ref _category, value);
                RaisePropertyChanged();
            }
        }

        private string _description;
        public string Description {
            get {
                return _description;
            }
            set {
                Set(() => Description, ref _description, value);
                RaisePropertyChanged();
            }
        }

        public string Day => StartTime.ToShortDateString() + " " + StartTime.DayOfWeek;

        public string Duration => EndTime == null ? "Ongoing" : (EndTime.Value - StartTime).Humanize(2);

        public override string ToString() => $"ID:{Id}\nStart Time:{StartTime.ToShortTimeString()}\nEnd Time:{EndTime?.ToShortTimeString()}\nCategory:{Category}\nDescription:{Description}\nDay:{Day}\nDuration:{Duration}";
    }
}
