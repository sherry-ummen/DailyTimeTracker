using System.Collections.Generic;
using CSharpFunctionalExtensions;
using DailyTimeTracker.Models;

namespace DailyTimeTracker.DatabaseLayer {
    public interface IDatabaseService {

        Result<bool> InsertActivity(Result<Activity> activity);

        Result<IEnumerable<Activity>> GetActivities();

        Result<IEnumerable<ActivityCategory>> GetCategories();
    }

}
