using System;
using System.Windows;
using DailyTimeTracker.BusinessLogic;
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
            IdleTimeNotifier.StartNotifier(5);
            IdleTimeNotifier.Idle += () => Dispatcher.BeginInvoke(new Action(() => {
                //Box.Text += "Idle now ";
            }));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e){
           
        }
    }
}
