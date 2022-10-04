using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public static class WeeklyRecurrencePatternExtensions {
        public static WeeklyRecurrencePattern IncludeDays(this WeeklyRecurrencePattern pattern, params DayOfWeek[] days)
            => pattern.IncludeDays(days.AsEnumerable());
        
        public static WeeklyRecurrencePattern IncludeDays(this WeeklyRecurrencePattern pattern, IEnumerable<DayOfWeek> days) {
            pattern.Days.UnionWith(days);
            return pattern;
        }

        public static WeeklyRecurrencePattern ExcludeDays(this WeeklyRecurrencePattern pattern, params DayOfWeek[] days)
            => pattern.ExcludeDays(days.AsEnumerable());

        public static WeeklyRecurrencePattern ExcludeDays(this WeeklyRecurrencePattern pattern, IEnumerable<DayOfWeek> days) {
            pattern.Days.ExceptWith(days);
            return pattern;
        }
    }
}