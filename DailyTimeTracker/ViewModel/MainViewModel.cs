using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DailyTimeTracker.DatabaseLayer;
using DailyTimeTracker.Models;
using DailyTimeTracker.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Configurations;

namespace DailyTimeTracker.ViewModel {
    public class MainViewModel : ViewModelBase {
        private readonly IDialogService _dialogService;
        private double _axisMax;
        private double _axisMin;
        private IDatabaseService _databaseService;

        public ObservableCollection<Activity> Activities {
            get;
            private set;
        }

        public ChartValues<Activity> Values { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }

        public double AxisMax {
            get { return _axisMax; }
            set {
                Set(() => AxisMax, ref _axisMax, value);
            }
        }
        public double AxisMin {
            get { return _axisMin; }
            set {
                Set(() => AxisMin, ref _axisMin, value);
            }
        }

        #region Commands

        public ICommand AddActivityCommand => new RelayCommand(AddActivityCommandExecute);

        private void AddActivityCommandExecute(){
            var activity = _dialogService.ShowAddActivtyDialog();
            _databaseService.InsertActivity(activity);
        }

        #endregion Commands

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDialogService dialogService, IDatabaseService databaseService) {
            _dialogService = dialogService;
            _databaseService = databaseService;
            var activities = databaseService.GetActivities();
            Activities = new ObservableCollection<Activity>(activities.IsSuccess ? activities.Value : Enumerable.Empty<Activity>());
            var mapper = Mappers.Xy<Activity>()
               .X(model => model.StartTime.Ticks / TimeSpan.FromDays(1).Ticks)   //use DateTime.Ticks as X
               .Y(model => ((model.EndTime ?? DateTime.Now) - model.StartTime).Hours);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<Activity>(mapper);
            AxisMin = 0;
            AxisMax = Activities.Count == 0 ? 9 : Activities.Max(model => ((model.EndTime ?? DateTime.Now) - model.StartTime).Hours + 2);

            ////lets set how to display the X Labels
            DateTimeFormatter = DateTimeFormatterString;

            //the values property will store our values array
            Values = new ChartValues<Activity>();
            Values.AddRange(Activities);

            AxisStep = 1;

        }

        private string DateTimeFormatterString(double value) {
            return new DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("dd-MMM-yy ddd");
        }
    }
}