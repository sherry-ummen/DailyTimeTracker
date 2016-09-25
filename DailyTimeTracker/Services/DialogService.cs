using System;
using System.Windows;
using DailyTimeTracker.Models;
using DailyTimeTracker.Views;
using LanguageExt;

namespace DailyTimeTracker.Services{
    public class DialogService : IDialogService {
        public Option<Activity> ShowAddActivtyDialog(){
            var window = new AddActivity{Owner = Application.Current.MainWindow /*TODO: Find a way to pass in as parameter*/};
            window.ShowDialog();
            return Option<Activity>.None;
        }
    }
}