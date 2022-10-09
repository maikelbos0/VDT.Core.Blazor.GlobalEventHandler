using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public static class WeeklyRecurrencePatternExtensions {
        public static WeeklyRecurrencePattern UseCalendarWeeks(this WeeklyRecurrencePattern pattern, DayOfWeek? firstDayOfWeek = null) {
            pattern.PeriodHandling = RecurrencePatternPeriodHandling.Calendar;
            if (firstDayOfWeek != null) {
                pattern.FirstDayOfWeek = firstDayOfWeek.Value;
            }
            return pattern;
        }

        public static WeeklyRecurrencePattern UseOngoingWeeks(this WeeklyRecurrencePattern pattern) {
            pattern.PeriodHandling = RecurrencePatternPeriodHandling.Ongoing;
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