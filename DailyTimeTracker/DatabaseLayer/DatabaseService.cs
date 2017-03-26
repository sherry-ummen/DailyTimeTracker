using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTimeTracker.Models;
using LiteDB;

namespace DailyTimeTracker.DatabaseLayer {
    public class DatabaseService : IDatabaseService {
        private static LiteDatabase _database;
        public static void Initialize() {
            if (_database == null) {
                var dbPath = Path.Combine(Path.GetFullPath(Environment.SpecialFolder.CommonApplicationData.ToString()), "3.0.0", "DailyTimeTracker.db");
                _database = new LiteDatabase(dbPath);
            }
                
        }

        public bool InsertActivity(Activity activity) {
            Initialize();
            var collection = _database.GetCollection<Activity>("Activities");
            return collection.Upsert(activity);
        }
    }
}
