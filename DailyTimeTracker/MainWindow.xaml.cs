using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DailyTimeTracker.BusinessLogic;
using DailyTimeTracker.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.SimpleChildWindow;

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
