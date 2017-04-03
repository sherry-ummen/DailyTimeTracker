using System;
using System.Windows;
using CSharpFunctionalExtensions;
using DailyTimeTracker.Models;
using DailyTimeTracker.ViewModel;
using DailyTimeTracker.Views;

namespace DailyTimeTracker.Services {
    public class DialogService : IDialogService {
        public Result<Activity> ShowAddActivtyDialog() {
            var window = new AddActivity { Owner = Application.Current.MainWindow /*TODO: Find a way to pass in as parameter*/};
            var activityViewModel = ViewModelLocator.AddActivityViewModel;
            window.DataContext = activityViewModel;
            window.ShowDialog();
            return activityViewModel.ReturnResult;
        }

        public Result<AfterIdleQueryViewModel> ShowAfterIdleQueryDialog(TimeSpan timeTaken) {
            var window = new AfterIdleQuery() { Owner = Application.Current.MainWindow };
            var afterIdleQueryViewModel = ViewModelLocator.AfterIdleQueryViewModel(timeTaken);
            window.DataContext = afterIdleQueryViewModel;
            window.ShowDialog();
            return afterIdleQueryViewModel.ReturnResult;
        }

        public void ShowErrorMessage(string title, string error) {
            MessageBox.Show(error, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ShowConfirmation(string title, string message) {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

        }
    }
}