using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public static class MonthlyRecurrencePatternExtensions {
        public static MonthlyRecurrencePattern IncludeDaysOfMonth(this MonthlyRecurrencePattern pattern, params int[] days)
            => pattern.IncludeDaysOfMonth(days.AsEnumerable());

        public static MonthlyRecurrencePattern IncludeDaysOfMonth(this MonthlyRecurrencePattern pattern, IEnumerable<int> days) {
            pattern.DaysOfMonth.UnionWith(days);
            return pattern;
        }

        public static MonthlyRecurrencePattern ExcludeDaysOfMonth(this MonthlyRecurrencePattern pattern, params int[] days)
            => pattern.ExcludeDaysOfMonth(days.AsEnumerable());

        public static MonthlyRecurrencePattern ExcludeDaysOfMonth(this MonthlyRecurrencePattern pattern, IEnumerable<int> days) {
            pattern.DaysOfMonth.ExceptWith(days);
            return pattern;
        }

        public static MonthlyRecurrencePattern IncludeDayOfWeek(this MonthlyRecurrencePattern pattern, WeekOfMonth weekOfMonth, DayOfWeek dayOfWeek)
            => pattern.IncludeDaysOfWeek((weekOfMonth, dayOfWeek));

        public static MonthlyRecurrencePattern IncludeDaysOfWeek(this MonthlyRecurrencePattern pattern, params (WeekOfMonth, DayOfWeek)[] days)
            => pattern.IncludeDaysOfWeek(days.AsEnumerable());

        public static MonthlyRecurrencePattern IncludeDaysOfWeek(this MonthlyRecurrencePattern pattern, IEnumerable<(WeekOfMonth, DayOfWeek)> days) {
            pattern.DaysOfWeek.UnionWith(days);
            return pattern;
        }

        public static MonthlyRecurrencePattern ExcludeDayOfWeek(this MonthlyRecurrencePattern pattern, WeekOfMonth weekOfMonth, DayOfWeek dayOfWeek)
            => pattern.ExcludeDaysOfWeek((weekOfMonth, dayOfWeek));

        public static MonthlyRecurrencePattern ExcludeDaysOfWeek(this MonthlyRecurrencePattern pattern, params (WeekOfMonth, DayOfWeek)[] days)
            => pattern.ExcludeDaysOfWeek(days.AsEnumerable());

        public static MonthlyRecurrencePattern ExcludeDaysOfWeek(this MonthlyRecurrencePattern pattern, IEnumerable<(WeekOfMonth, DayOfWeek)> days) {
            pattern.DaysOfWeek.ExceptWith(days);
            return pattern;
        }
    }
}