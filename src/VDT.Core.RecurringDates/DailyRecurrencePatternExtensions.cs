namespace VDT.Core.RecurringDates {
    public static class DailyRecurrencePatternExtensions {
        public static DailyRecurrencePattern IncludeWeekends(this DailyRecurrencePattern pattern) {
            pattern.WeekendHandling = RecurrencePatternWeekendHandling.Include;
            return pattern;
        }

        public static DailyRecurrencePattern SkipWeekends(this DailyRecurrencePattern pattern) {
            pattern.WeekendHandling = RecurrencePatternWeekendHandling.Skip;
            return pattern;
        }

        public static DailyRecurrencePattern AdjustWeekendsToMonday(this DailyRecurrencePattern pattern) {
            pattern.WeekendHandling = RecurrencePatternWeekendHandling.AdjustToMonday;
            return pattern;
        }
    }
}