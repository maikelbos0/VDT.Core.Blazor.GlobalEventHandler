using System;

namespace VDT.Core.RecurringDates {
    public abstract class RecurrencePattern {
        public int Interval { get; }
        public DateTime Start { get; }

        public RecurrencePattern(int interval, DateTime start) {
            if (interval < 1) {
                throw new ArgumentOutOfRangeException(nameof(interval), $"Expected {nameof(interval)} to be at least 1 but found {interval}.");
            }

            Interval = interval;
            Start = start;
        }

        public abstract bool IsValid(DateTime date);
    }
}