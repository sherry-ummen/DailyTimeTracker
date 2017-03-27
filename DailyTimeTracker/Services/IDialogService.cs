using CSharpFunctionalExtensions;
using DailyTimeTracker.Models;

namespace DailyTimeTracker.Services {
    public interface IDialogService{
        Result<Activity> ShowAddActivtyDialog();
        void ShowErrorMessage(string title, string error);
        bool ShowConfirmation(string title, string message);
    }
}
