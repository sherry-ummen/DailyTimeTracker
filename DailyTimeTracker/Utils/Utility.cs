using System;
using System.Windows;

namespace DailyTimeTracker.Utils {
    public static class Utility {

        public static void DispatchIt(Action action) {
            if (!Application.Current.Dispatcher.CheckAccess())
                Application.Current.Dispatcher.Invoke(new Action(() => {
                    action?.Invoke();
                }));
            else {
                action?.Invoke(); 
            }
        }
    }
}
