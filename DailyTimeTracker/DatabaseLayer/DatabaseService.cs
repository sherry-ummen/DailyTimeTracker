﻿using System;
using System.Collections.Generic;
using System.IO;
using CSharpFunctionalExtensions;
using DailyTimeTracker.Models;
using LiteDB;

namespace DailyTimeTracker.DatabaseLayer {
    public class DatabaseService : IDatabaseService {
        private static LiteDatabase _database;
        public static void Initialize() {
            if (_database == null) {
                var dbdrive = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "DailyTimeTracker", "3.0.0");
                if (!Directory.Exists(dbdrive))
                    Directory.CreateDirectory(dbdrive);
                var dbPath = Path.Combine(dbdrive, "DailyTimeTracker.db");
                _database = new LiteDatabase(dbPath);
                StoreDefualtValues();
            }

        }

        private static void StoreDefualtValues() {
            var categoryCollection = _database.GetCollection<ActivityCategory>("Categories");
            categoryCollection.Upsert(new ActivityCategory() { Id = 1, Category = "Work" });
            categoryCollection.Upsert(new ActivityCategory() { Id = 2, Category = "Personal" });
            categoryCollection.Upsert(new ActivityCategory() { Id = 3, Category = "Lunch" });
        }

        public Result<IEnumerable<Activity>> GetActivities() {
            Initialize();
            var collection = _database.GetCollection<Activity>("Activities");
            return Result.Ok<IEnumerable<Activity>>(collection.FindAll());
        }

        public Result<IEnumerable<ActivityCategory>> GetCategories() {
            Initialize();
            var collection = _database.GetCollection<ActivityCategory>("Categories");
            return Result.Ok<IEnumerable<ActivityCategory>>(collection.FindAll());
        }

        public Result<bool> InsertActivity(Result<Activity> activity) {
            Initialize();
            var collection = _database.GetCollection<Activity>("Activities");
            if (activity.IsSuccess)
                return collection.Upsert(activity.Value) ? Result.Ok<bool>(true) : Result.Fail<bool>("Failed to insert activity " + activity);
            return Result.Fail<bool>(activity.Error);
        }
    }
}