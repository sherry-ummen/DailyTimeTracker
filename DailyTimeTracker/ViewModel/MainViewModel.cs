using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using DailyTimeTracker.Models;
using DailyTimeTracker.Services;
using DailyTimeTracker.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Configurations;
using MahApps.Metro.SimpleChildWindow;

namespace DailyTimeTracker.ViewModel {
    public class MainViewModel : ViewModelBase {
        private readonly IDialogService _dialogService;
        private double _axisMax;
        private double _axisMin;

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
            _dialogService.ShowAddActivtyDialog();
        }

        #endregion Commands

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDialogService dialogService) {
            _dialogService = dialogService;
            var category = new ActivityCategory() { Category = "Work", Id = 1 };
            Activities = new ObservableCollection<Activity>()
            {
                new Activity() {Category = category,Description = "Something aljdsfhkjlahds fkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfkjhasdk jhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsjhakjsdhkfkgj lkjlksdjlkfjsldfjlksdjflksdlkfjslkdfjlksdjfklsjdklfjslkdfjlsdjflsjdlfjslkdfjlskdflkjsdlkfjslkdfjlksdflksdfjasd", StartTime = DateTime.Now},
                new Activity() {Category = category,Description = "Something", StartTime = DateTime.Now.AddDays(-2), EndTime = DateTime.Now.AddDays(-2).AddHours(8)},
                new Activity() {Category = category,Description = "Something", StartTime = DateTime.Now.AddDays(-4), EndTime = DateTime.Now.AddDays(-4).AddHours(5)},
                new Activity() {Category = category,Description = "Something", StartTime = DateTime.Now.AddDays(-5), EndTime = DateTime.Now.AddDays(-5).AddHours(2).AddMinutes(10)},
            };

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