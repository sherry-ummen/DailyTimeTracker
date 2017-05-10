using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CSharpFunctionalExtensions;
using DailyTimeTracker.DatabaseLayer;
using DailyTimeTracker.Models;
using DailyTimeTracker.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Configurations;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Threading;
using DailyTimeTracker.BusinessLogic;
using LiveCharts.Helpers;

namespace DailyTimeTracker.ViewModel {
    public class MainViewModel : ViewModelBase {
        private readonly IDialogService _dialogService;
        private double _axisMax;
        private double _axisMin;
        private IDatabaseService _databaseService;
        private Activity _lastActivity;

        private ObservableCollection<Activity> _activities;
        private List<Activity> _selectedItems = Enumerable.Empty<Activity>().ToList();

        public ObservableCollection<Activity> Activities {
            get { return _activities; }
            set { Set(() => Activities, ref _activities, value); }
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

        private void AddActivityCommandExecute() {
            var activity = _dialogService.ShowAddActivtyDialog();
            AddActivity(activity);
        }

        public ICommand DeleteActivityCommand => new RelayCommand<Activity>((activity) => DeleteActivityCommandExecute(activity));

        public ICommand MergeActivitiesCommand => new RelayCommand<object>((f) => {
            var t = (System.Collections.IList)f;
            var items = t.Cast<Activity>().ToArray();
            if (items.Length <= 1) return;
            if (!_dialogService.ShowConfirmation("Merge Activities", "Are you sure you would like to merge these activities?")) return;
            var orderedItems = items.OrderByDescending(x => x.StartTime).ToArray();
            var firstItem = orderedItems.First();
            var lastItem = orderedItems.Last();
            firstItem.StartTime = lastItem.StartTime;
            firstItem.Description = lastItem.Description;
            firstItem.Category = lastItem.Category;
            items.Except(new [] { firstItem }).ForEach(x => Activities.Remove(x));
            _databaseService.UpdateActivity(Result.Ok(firstItem));
            //UpdateList();
        });

        private void DeleteActivityCommandExecute(Activity activity) {
            if (_dialogService.ShowConfirmation("Deletion Confirmation", "Are you sure you want to delete?"))
                Activities.Remove(activity);
        }

        #endregion Commands

        private void AddActivity(Result<Activity> activity) {
            if (activity.IsSuccess) {
                if (_lastActivity != null) {
                    if (_lastActivity.EndTime == null) {
                        _lastActivity.EndTime = DateTime.Now;
                        _databaseService.UpdateActivity(Result.Ok<Activity>(_lastActivity));
                    }
                }
                Activities.Insert(0, activity.Value);
                UpdateList();
            } else {
                _dialogService.ShowErrorMessage("Failure!", $"Failed to add the activity with error :\n{activity.Error}");
            }
        }

        private void UpdateList() {
            var activities = _databaseService.GetActivities();
            Activities = new ObservableCollection<Activity>(activities.IsSuccess ? activities.Value.Reverse() : Enumerable.Empty<Activity>());
            Activities.CollectionChanged += Activities_CollectionChanged;
            _lastActivity = Activities.FirstOrDefault();
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDialogService dialogService, IDatabaseService databaseService, IIdleTimeNotifier idleTimeNotifier) {
            _dialogService = dialogService;
            _databaseService = databaseService;
            idleTimeNotifier.StartNotifier(300); // Take this from configuration or settings
            idleTimeNotifier.IdleTimeBegins += IdleTimeNotifierOnIdleTimeBegins;
            idleTimeNotifier.IdleTimeEnds += IdleTimeNotifierOnIdleTimeEnds;
            Activities = new ObservableCollection<Activity>();
            UpdateList();
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

        private void IdleTimeNotifierOnIdleTimeBegins() {
            var activity = new Activity() {
                Category = _databaseService.GetCategories().Value.ToList().Where(x => x.Category == "Idle").First(),
                Description = "Idle Time",
                StartTime = DateTime.Now,
            };

            //TODO: Get rid of direct call to dispatcher. Wrap it.
            Application.Current.Dispatcher.Invoke(new Action(() => {
                AddActivity(Result.Ok(activity));
            }));
        }

        private void IdleTimeNotifierOnIdleTimeEnds(TimeSpan timeTaken) {
            // Ask user about what you were doing
            // Do you want to continue previous task
            // Do you want to start new task
            Result<AfterIdleQueryViewModel> vm = Result.Fail<AfterIdleQueryViewModel>("Default Value");
            Application.Current.Dispatcher.Invoke(() => {
                vm = _dialogService.ShowAfterIdleQueryDialog(timeTaken); //TODO: timeTaken should be displayed as well
            });
            if (vm.IsFailure) return;
            AfterIdleQueryViewModel idleQuery = vm.Value;
            _lastActivity.Category = idleQuery.IdleActivity.Category;
            _lastActivity.Description = idleQuery.IdleActivity.Description;
            Activity activity = null;
            if (idleQuery.IsNewTask) {
                activity = new Activity() {
                    Category = idleQuery.NewActivity.Category,
                    Description = idleQuery.NewActivity.Description,
                    StartTime = DateTime.Now,
                };
            } else {
                activity = Activities.Count > 1
                    ? Activities.ElementAt(1)
                    : new Activity() {
                        Category = _databaseService.GetCategories().Value.First(x => x.Category == "Work"),
                        Description = "Some work!",
                        StartTime = DateTime.Now
                    };
                activity.StartTime = DateTime.Now;
                if (Activities.Count > 1) {
                    activity.Id = 0;
                    activity.EndTime = null;
                }
            }
            Application.Current.Dispatcher.Invoke(new Action(() => {
                AddActivity(Result.Ok(activity));
            }));
        }

        private void Activities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action) {
                case NotifyCollectionChangedAction.Add: {
                        foreach (var activity in e.NewItems) {
                            _databaseService.InsertActivity(Result.Ok((Activity)activity));

                        }
                        break;
                    }
                case NotifyCollectionChangedAction.Remove: {
                        foreach (var activity in e.OldItems) {
                            var result = _databaseService.DeleteActivity(Maybe<Activity>.From(activity as Activity));
                            if (result.IsSuccess) _lastActivity = Activities.FirstOrDefault();
                            if (result.IsFailure) {
                                _dialogService.ShowErrorMessage("Failed to deleted activity!", result.Error);
                            }
                        }
                        break;
                    }
            }
        }

        private string DateTimeFormatterString(double value) {
            return new DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("dd-MMM-yy ddd");
        }
    }
}