using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using MahApps.Metro.IconPacks;

namespace DailyTimeTracker.ViewModel {
    public class ShellViewModel : ViewModelBase {
        private static readonly ObservableCollection<MenuItem> AppMenu = new ObservableCollection<MenuItem>();
        private static readonly ObservableCollection<MenuItem> AppOptionsMenu = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> Menu => AppMenu;
        public ObservableCollection<MenuItem> OptionsMenu => AppOptionsMenu;

        public ShellViewModel() {
            // Build the menus
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.Home }, Text = "Home", NavigationDestination = new Uri("Views/Home.xaml", UriKind.RelativeOrAbsolute) });
            this.Menu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.PieChart }, Text = "Reports", NavigationDestination = new Uri("Views/Home.xaml", UriKind.RelativeOrAbsolute) });

            this.OptionsMenu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.Cogs }, Text = "Settings", NavigationDestination = new Uri("Views/Home.xaml", UriKind.RelativeOrAbsolute) });
            this.OptionsMenu.Add(new MenuItem() { Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.InfoCircle }, Text = "About", NavigationDestination = new Uri("Views/Home.xaml", UriKind.RelativeOrAbsolute) });
        }
    }
}