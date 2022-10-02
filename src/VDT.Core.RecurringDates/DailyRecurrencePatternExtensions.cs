namespace VDT.Core.RecurringDates {
    public static class DailyRecurrencePatternExtensions {
        public static DailyRecurrencePattern IncludeWeekends(this DailyRecurrencePattern pattern) {
            pattern.IncludingWeekends = true;
            return pattern;
        }

        public static DailyRecurrencePattern ExcludeWeekends(this DailyRecurrencePattern pattern) {
            pattern.IncludingWeekends = false;
            return pattern;
        }
    }
}