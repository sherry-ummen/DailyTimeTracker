using DailyTimeTracker.DatabaseLayer;
using Moq;

namespace DailyTimeTrackerTests.Mocks {
    public class MockedDatabaseServices {

        public IDatabaseService Setup() {
            return new Mock<IDatabaseService>().Object;
        }

    }
}
