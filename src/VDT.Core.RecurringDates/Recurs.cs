namespace VDT.Core.RecurringDates {
    public static class Recurs {
        public static Recurrence Every(int interval = 1)
            => new Recurrence() {
                Interval = interval
            };
    }
}