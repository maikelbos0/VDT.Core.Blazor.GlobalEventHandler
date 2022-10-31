using System;

namespace VDT.Core.RecurringDates {
    public class RecurrenceBuilder {
        public int Interval { get; private set; } = 1;
        public DateTime StartDate { get; private set; } = DateTime.MinValue;
        public DateTime EndDate { get; private set; } = DateTime.MaxValue;

        public RecurrenceBuilder RepeatsEvery(int interval) {
            if (interval < 1) {
                throw new ArgumentOutOfRangeException(nameof(interval), interval, $"Expected {nameof(interval)} to be at least 1.");
            }

            Interval = interval;
            return this;
        }

        public RecurrenceBuilder StartsOn(DateTime startDate) {
            StartDate = startDate.Date;
            return this;
        }

        public RecurrenceBuilder EndsOn(DateTime endDate) {
            EndDate = endDate.Date;
            return this;
        }
    }
}
