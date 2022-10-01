namespace VDT.Core.RecurringDates {
    public static class RecurrenceExtensions {
        public static Recurrence Days(this Recurrence recurrence, Action<DailyRecurrenceOptions> optionsBuilder = null) {
            var options = new DailyRecurrenceOptions();
            optionsBuilder?.Invoke(options);
            recurrence.Options = options;
            return recurrence;
        }

        public static Recurrence StartsOn(this Recurrence recurrence, DateTime startDate) {
            recurrence.StartDate = startDate;
            return recurrence;
        }

        public static Recurrence EndsOn(this Recurrence recurrence, DateTime endDate) {
            recurrence.EndDate = endDate;
            return recurrence;
        }
    }
}