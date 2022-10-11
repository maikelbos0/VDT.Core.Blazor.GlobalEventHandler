using System;

namespace VDT.Core.RecurringDates {
    public static class RecurrenceExtensions {
        public static Recurrence StartsOn(this Recurrence recurrence, DateTime startDate) {
            recurrence.Start = startDate;
            return recurrence;
        }

        public static Recurrence EndsOn(this Recurrence recurrence, DateTime endDate) {
            recurrence.End = endDate;
            return recurrence;
        }

        public static Recurrence Days(this Recurrence recurrence, Action<DailyRecurrencePattern>? patternBuilder = null) {
            var pattern = new DailyRecurrencePattern(recurrence);
            patternBuilder?.Invoke(pattern);
            recurrence.Pattern = pattern;
            return recurrence;
        }

        public static Recurrence Weeks(this Recurrence recurrence, Action<WeeklyRecurrencePattern>? patternBuilder = null) {
            var pattern = new WeeklyRecurrencePattern(recurrence);
            patternBuilder?.Invoke(pattern);
            recurrence.Pattern = pattern;
            return recurrence;
        }
    }
}