using System.Windows;
using CSharpFunctionalExtensions;
using DailyTimeTracker.Models;
using DailyTimeTracker.ViewModel;
using DailyTimeTracker.Views;

namespace DailyTimeTracker.Services {
    public class DialogService : IDialogService {
        public Result<Activity> ShowAddActivtyDialog(){
            var window = new AddActivity{Owner = Application.Current.MainWindow /*TODO: Find a way to pass in as parameter*/};
            var activityViewModel = ViewModelLocator.AddActivityViewModel;
            window.DataContext = activityViewModel;
            window.ShowDialog();
            return activityViewModel.ReturnResult;
        }
    }
}