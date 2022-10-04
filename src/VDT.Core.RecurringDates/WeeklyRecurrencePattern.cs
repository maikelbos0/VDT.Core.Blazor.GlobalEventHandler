using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePattern : IRecurrencePattern {
        public HashSet<DayOfWeek> Days { get; set; } = new HashSet<DayOfWeek>();

        DateTime? IRecurrencePattern.GetFirst(int interval, DateTime start, DateTime from) {
            throw new NotImplementedException();
        }

        DateTime? IRecurrencePattern.GetNext(int interval, DateTime current) {
            throw new NotImplementedException();
        }

        internal Dictionary<DayOfWeek, int> GetDayMap() {
            var days = Days.OrderBy(day => day).ToList();
            var dayMap = new Dictionary<DayOfWeek, int>();

            for (var i = 0; i < days.Count - 1; i++) {
                dayMap[days[i]] = days[i + 1] - days[i];
            }

            dayMap[days[days.Count - 1]] = 7 + days[0] - days[days.Count - 1];

            return dayMap;
        }
    }
}