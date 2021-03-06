﻿using System.Collections.Generic;
using CSharpFunctionalExtensions;
using DailyTimeTracker.Models;

namespace DailyTimeTracker.DatabaseLayer {
    public interface IDatabaseService {

        Result<bool> InsertActivity(Result<Activity> activity);

        Result<bool> UpdateActivity(Result<Activity> activity);

        Result<IEnumerable<Activity>> GetActivities();

        Result<IEnumerable<Activity>> GetActivitiesForMonth(int month);

        Result<IEnumerable<ActivityCategory>> GetCategories();

        Result<bool> DeleteActivity(Maybe<Activity> activity);
    }

}
