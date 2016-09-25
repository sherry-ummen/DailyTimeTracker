using System;
using System.Collections.Generic;
using System.Windows.Input;
using DailyTimeTracker.Models;
using DailyTimeTracker.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using LanguageExt;

namespace DailyTimeTracker.ViewModel {
    public class AddActivityViewModel : ViewModelBase {
        private ActivityCategory _category;
        private string _description;
        private List<ActivityCategory> _categories;

        public Action OnExit;

        public ActivityCategory Category {
            get { return _category; }
            set { Set(() => Category, ref _category, value); }
        }

        public string Description {
            get { return _description; }
            set { Set(() => Description, ref _description, value); }
        }

        public List<ActivityCategory> Categories {
            get { return _categories; }
            set { Set(() => Categories, ref _categories, value); }
        }

        public ICommand OkCommand => new RelayCommand<IClosable>(OkCommandExecute);

        public ICommand CancelCommand => new RelayCommand<IClosable>(CancelCommandExecute);

        public Option<Activity> Result { get; private set; } = Option<Activity>.None;

        private void CancelCommandExecute(IClosable closable) {
            closable.Close();
        }

        private void OkCommandExecute(IClosable closable) {
            // Set the result
            var result = new Activity();
            result.StartTime = DateTime.Now;
            result.Category = Category;
            result.Description = Description;
            Result = Option<Activity>.Some(result);
            closable.Close();
        }

        public AddActivityViewModel() {
            _categories = new List<ActivityCategory>();
            _categories.AddRange(new[] { new ActivityCategory() { Category = "Work", Id = 1 }, new ActivityCategory() { Category = "Personal", Id = 2 } });
        }

    }
}
