using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CSharpFunctionalExtensions;
using DailyTimeTracker.DatabaseLayer;
using DailyTimeTracker.Models;
using DailyTimeTracker.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace DailyTimeTracker.ViewModel {
    public class AfterIdleQueryViewModel : ViewModelBase {
        private readonly IDatabaseService _databaseService;



        public AfterIdleQueryViewModel(IDatabaseService databaseService) {
            _databaseService = databaseService;
            var categories = _databaseService.GetCategories().Value;
            var activityCategories = categories as IList<ActivityCategory> ?? categories.ToList();
            IdleActivity = new SimpleActivityViewModel(activityCategories, "Idle Time.");
            NewActivity = new SimpleActivityViewModel(new List<ActivityCategory>(activityCategories), "");
        }

        public bool IsNewTask { get; set; } = false;
        public bool IsContinuation { get; set; } = true;
        public Activity DuringIdle { get; set; }
        public Activity NewTask { get; set; }

        public SimpleActivityViewModel IdleActivity { get; set; }

        public SimpleActivityViewModel NewActivity { get; set; }

        public ICommand OkCommand => new RelayCommand<IClosable>(OkCommandExecute);

        public ICommand CancelCommand => new RelayCommand<IClosable>(CancelCommandExecute);

        public Result<AfterIdleQueryViewModel> ReturnResult { get; private set; } = Result.Fail<AfterIdleQueryViewModel>("Default result");

        private void CancelCommandExecute(IClosable closable) {
            closable.Close();
        }

        private void OkCommandExecute(IClosable closable) {
            ReturnResult = Result.Ok(this);
            closable.Close();
        }

    }

    public class SimpleActivityViewModel : ViewModelBase {
        private string _description;
        private IEnumerable<ActivityCategory> _categories;
        private ActivityCategory _category;

        public ActivityCategory Category {
            get { return _category; }
            set { Set(() => Category, ref _category, value); }
        }
        public string Description {
            get { return _description; }
            set { Set(() => Description, ref _description, value); }
        }

        public IEnumerable<ActivityCategory> Categories {
            get { return _categories; }
            set { Set(() => Categories, ref _categories, value); }
        }

        public SimpleActivityViewModel(IEnumerable<ActivityCategory> categories, string description) {
            _categories = categories;
            Category = categories.First();
            _description = description;
        }
    }
}
