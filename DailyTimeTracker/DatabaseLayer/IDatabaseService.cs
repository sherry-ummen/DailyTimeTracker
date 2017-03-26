using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTimeTracker.Models;

namespace DailyTimeTracker.DatabaseLayer {
    interface IDatabaseService {

        bool InsertActivity(Activity activity);
    }
}
