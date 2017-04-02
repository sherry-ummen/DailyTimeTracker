using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using DailyTimeTracker.Native;

namespace DailyTimeTracker.BusinessLogic {
    public class IdleTimeNotifier : IIdleTimeNotifier {

        private bool _isNotifierStarted;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isNotified;

        public void StartNotifier(int idleTimeInSeconds) {
            if (_isNotifierStarted)
                return;
            _isNotifierStarted = true;
            _cancellationTokenSource = new CancellationTokenSource();
            new TaskFactory().StartNew(() => {
                while (!_cancellationTokenSource.IsCancellationRequested) {
                    var time = Win32.GetIdleTime();
                    //Debug.WriteLine(time);
                    if (time > idleTimeInSeconds) {
                        if (!_isNotified) {
                            //Debug.WriteLine("Notified");
                            IdleTimeBegins?.Invoke();
                            _isNotified = true;
                        }
                    } else {
                        if (_isNotified && time == 0) {
                            //Debug.WriteLine("Active");
                            IdleTimeEnds?.Invoke();
                        }
                        _isNotified = false;
                    }
                }
            });
        }

        public event Action IdleTimeBegins;
        public event Action IdleTimeEnds;
    }
}
