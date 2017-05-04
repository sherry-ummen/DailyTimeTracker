using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DailyTimeTracker.Views;
using MahApps.Metro.Controls;
using MenuItem = DailyTimeTracker.ViewModel.MenuItem;

namespace DailyTimeTracker {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();
            // Navigate to the home page.
            Navigation.Navigation.Frame = new Frame(); //SplitViewFrame;
            Navigation.Navigation.Frame.Navigated += SplitViewFrame_OnNavigated;
            this.Loaded += (sender, args) => Navigation.Navigation.Navigate(new Home());
        }

        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e) {
            HamburgerMenuControl.Content = e.Content;
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

        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e) {
            var menuItem = e.ClickedItem as MenuItem;
            if(menuItem != null && menuItem.IsNavigation) {
                Navigation.Navigation.Navigate(menuItem.NavigationDestination);
            }
        }
    }
}
