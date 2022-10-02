using System;

namespace VDT.Core.RecurringDates {
    public static class RecurrenceExtensions {
        public static Recurrence Days(this Recurrence recurrence, Action<DailyRecurrencePattern>? patternBuilder = null) {
            var pattern = new DailyRecurrencePattern();
            patternBuilder?.Invoke(pattern);
            recurrence.Pattern = pattern;
            return recurrence;
        }

        public static Recurrence StartsOn(this Recurrence recurrence, DateTime startDate) {
            recurrence.Start = startDate;
            return recurrence;
        }

        public static Recurrence EndsOn(this Recurrence recurrence, DateTime endDate) {
            recurrence.End = endDate;
            return recurrence;
        }
    }
}