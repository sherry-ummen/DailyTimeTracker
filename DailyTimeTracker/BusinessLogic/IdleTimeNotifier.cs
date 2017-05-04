using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DailyTimeTracker.BusinessLogic.Native;

namespace DailyTimeTracker.BusinessLogic
{
    public class IdleTimeNotifier : IIdleTimeNotifier
    {

        private bool _isNotifierStarted;
        private MouseInput _mouse;
        private KeyboardInput _keyboard;
        private volatile bool _idleTime = false;

        public void StartNotifier(int idleTimeInSeconds) {
            if(_isNotifierStarted)
                return;
            _isNotifierStarted = true;

            _keyboard = new KeyboardInput();
            _keyboard.KeyBoardKeyPressed += OnNonIdleState;
            _mouse = new MouseInput();
            _mouse.MouseMoved += OnNonIdleState;

            CheckForIdleTime(idleTimeInSeconds);
        }

        void CheckForIdleTime(int idleTimeInSeconds) {
            var task = Task.Run(() => {
                while(true) {
                    if((DateTime.Now - StartTime).TotalSeconds > idleTimeInSeconds && !IsNotified && !_idleTime) {
                        Debug.WriteLine($"Idle time begins:{(DateTime.Now - StartTime).TotalSeconds}");
                        IdleTimeBegins?.Invoke();
                        _idleTime = true;
                        IsNotified = true;
                    } else {
                        if(!_idleTime && IsNotified) {
                            Debug.WriteLine($"Idle time ends:{(DateTime.Now - StartTime).TotalSeconds}");
                            IdleTimeEnds?.Invoke(DateTime.Now.Subtract(StartTime));
                            StartTime = DateTime.Now;
                            IsNotified = false;
                        }
                    }
                }
            });
            task.ContinueWith((t) => {
                //TODO: Some error handling code
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private bool IsNotified { get; set; }

        private DateTime StartTime { get; set; } = DateTime.Now;

        private void OnNonIdleState(object sender, EventArgs e) {
            if(!IsNotified) StartTime = DateTime.Now;
            _idleTime = false;
        }

        public event Action IdleTimeBegins;
        public event Action<TimeSpan> IdleTimeEnds;
    }
}
