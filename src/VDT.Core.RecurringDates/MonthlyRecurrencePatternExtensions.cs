using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public static class MonthlyRecurrencePatternExtensions {
        public static MonthlyRecurrencePattern UseCalendarMonths(this MonthlyRecurrencePattern pattern) {
            pattern.PeriodHandling = RecurrencePatternPeriodHandling.Calendar;
            return pattern;
        }

        public static MonthlyRecurrencePattern UseOngoingMonths(this MonthlyRecurrencePattern pattern) {
            pattern.PeriodHandling = RecurrencePatternPeriodHandling.Ongoing;
            return pattern;
        }

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
    }
}