using System.Windows;
using MahApps.Metro.Controls;

namespace DailyTimeTracker {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            //IdleTimeNotifier.StartNotifier(5);
            //IdleTimeNotifier.Idle += () => Dispatcher.BeginInvoke(new Action(() => {

            //    Application.Current.MainWindow.Activate();
            //    Application.Current.MainWindow.Topmost = true;
            //    Application.Current.MainWindow.ShowActivated = false;
            //    if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            //        Application.Current.MainWindow.WindowState = WindowState.Normal;
            //    /*
            //     On idle record the time of idle
            //     when user comes back give him the option to contiue what he was doing earlier or start new task
            //     */

            //}));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {

        }
    }
}
