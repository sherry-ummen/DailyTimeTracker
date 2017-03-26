using System.Windows;
using DailyTimeTracker.DatabaseLayer;

namespace DailyTimeTracker {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            DatabaseService.Initialize(); 
            base.OnStartup(e);
        }
    }
}
