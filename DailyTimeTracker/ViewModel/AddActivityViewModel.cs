using System;
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
    public class AddActivityViewModel : ViewModelBase {
        private ActivityCategory _category;
        private string _description;
        private IEnumerable<ActivityCategory> _categories;

        public bool IsNewTask { get; set; } = true;

        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime? EndTime { get; set; }

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

        public ICommand OkCommand => new RelayCommand<IClosable>(OkCommandExecute);

        public ICommand CancelCommand => new RelayCommand<IClosable>(CancelCommandExecute);

        public Result<Activity> ReturnResult { get; private set; } = Result.Fail<Activity>("Defualt result");

        private void CancelCommandExecute(IClosable closable) {
            closable.Close();
        }

        private void OkCommandExecute(IClosable closable) {
            // Set the result
            var result = new Activity();
            if (IsNewTask) {
                result.StartTime = DateTime.Now;
                result.Category = Category;
                result.Description = Description;
            } else {
                result.StartTime = StartTime;
                result.EndTime = EndTime;
                result.Category = Category;
                result.Description = Description;
            }
            ReturnResult = Result.Ok<Activity>(result);
            closable.Close();
        }

        public AddActivityViewModel(IDatabaseService databaseService) {
            _categories = databaseService.GetCategories().IsSuccess ? databaseService.GetCategories().Value : Enumerable.Empty<ActivityCategory>();
            Category = Categories.First();
        }

    }
}
