namespace VDT.Core.RecurringDates {
    public static class DailyRecurrenceOptionsExtensions {
        public static DailyRecurrenceOptions IncludeWeekends(this DailyRecurrenceOptions options) {
            options.IncludingWeekends = true;
            return options;
        }

        public static DailyRecurrenceOptions ExcludeWeekends(this DailyRecurrenceOptions options) {
            options.IncludingWeekends = false;
            return options;
        }
    }
}