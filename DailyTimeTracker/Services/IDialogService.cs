using System;
using CSharpFunctionalExtensions;
using DailyTimeTracker.Models;
using DailyTimeTracker.ViewModel;

namespace DailyTimeTracker.Services {
    public interface IDialogService{
        Result<Activity> ShowAddActivtyDialog();
        Result<AfterIdleQueryViewModel> ShowAfterIdleQueryDialog(TimeSpan timeTaken);
        void ShowErrorMessage(string title, string error);
        bool ShowConfirmation(string title, string message);
    }
}
