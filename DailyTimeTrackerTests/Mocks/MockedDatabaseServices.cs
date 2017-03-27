using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTimeTracker.DatabaseLayer;
using Moq;

namespace DailyTimeTrackerTests.Mocks {
    public class MockedDatabaseServices {

        public IDatabaseService Setup() {
            return new Mock<IDatabaseService>().Object;
        }

    }
}
