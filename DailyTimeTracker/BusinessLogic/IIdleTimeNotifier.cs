using System;

namespace DailyTimeTracker.BusinessLogic {
    public interface IIdleTimeNotifier {
        void StartNotifier(int idleTimeInSeconds);

        event Action IdleTimeBegins;

        event Action<TimeSpan> IdleTimeEnds;
    }
}
