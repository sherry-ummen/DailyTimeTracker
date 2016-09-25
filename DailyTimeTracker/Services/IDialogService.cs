using DailyTimeTracker.Models;
using LanguageExt;

namespace DailyTimeTracker.Services {
    public interface IDialogService{
        Option<Activity> ShowAddActivtyDialog();
    }
}
