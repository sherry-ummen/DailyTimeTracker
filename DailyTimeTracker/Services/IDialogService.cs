using CSharpFunctionalExtensions;
using DailyTimeTracker.Models;

namespace DailyTimeTracker.Services {
    public interface IDialogService{
        Result<Activity> ShowAddActivtyDialog();
    }
}
