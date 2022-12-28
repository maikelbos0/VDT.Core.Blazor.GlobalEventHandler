using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public static class WeeklyRecurrencePatternExtensions {
        public static WeeklyRecurrencePattern UseFirstDayOfWeek(this WeeklyRecurrencePattern pattern, DayOfWeek firstDayOfWeek) {
            pattern.FirstDayOfWeek = firstDayOfWeek;
            return pattern;
        }

        public static WeeklyRecurrencePattern IncludeDaysOfWeek(this WeeklyRecurrencePattern pattern, params DayOfWeek[] days)
            => pattern.IncludeDaysOfWeek(days.AsEnumerable());
        
        public static WeeklyRecurrencePattern IncludeDaysOfWeek(this WeeklyRecurrencePattern pattern, IEnumerable<DayOfWeek> days) {
            pattern.DaysOfWeek.UnionWith(days);
            return pattern;
        }

        public static WeeklyRecurrencePattern ExcludeDaysOfWeek(this WeeklyRecurrencePattern pattern, params DayOfWeek[] days)
            => pattern.ExcludeDaysOfWeek(days.AsEnumerable());

        public static WeeklyRecurrencePattern ExcludeDaysOfWeek(this WeeklyRecurrencePattern pattern, IEnumerable<DayOfWeek> days) {
            pattern.DaysOfWeek.ExceptWith(days);
            return pattern;
        }
    }
}