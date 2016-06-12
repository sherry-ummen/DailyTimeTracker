using System;
using System.Threading;
using System.Threading.Tasks;
using DailyTimeTracker.Native;

namespace DailyTimeTracker.BusinessLogic {
    public static class IdleTimeNotifier {

        private static bool _isNotifierStarted;
        private static CancellationTokenSource _cancellationTokenSource;
        private static bool _isNotified;

        public static event Action Idle;

        public static void StartNotifier(int idleTime) {
            if (_isNotifierStarted)
                return;
            _isNotifierStarted = true;
            _cancellationTokenSource = new CancellationTokenSource();
            new TaskFactory().StartNew(() => {
                while (!_cancellationTokenSource.IsCancellationRequested) {
                    if (Win32.GetIdleTime() > idleTime) {
                        if (!_isNotified) {
                            Notify();
                            _isNotified = true;
                        }
                    } else {
                        _isNotified = false;
                    }
                }
            });
        }

        public static void Notify() {
            Idle?.Invoke();
        }
    }
}
