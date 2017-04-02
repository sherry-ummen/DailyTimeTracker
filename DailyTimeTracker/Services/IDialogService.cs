using CSharpFunctionalExtensions;
using DailyTimeTracker.Models;
using DailyTimeTracker.ViewModel;

namespace DailyTimeTracker.Services {
    public interface IDialogService{
        Result<Activity> ShowAddActivtyDialog();
        Result<AfterIdleQueryViewModel> ShowAfterIdleQueryDialog();
        void ShowErrorMessage(string title, string error);
        bool ShowConfirmation(string title, string message);
    }
}
