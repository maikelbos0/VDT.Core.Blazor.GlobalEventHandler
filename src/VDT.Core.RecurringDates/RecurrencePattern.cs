using System;

namespace VDT.Core.RecurringDates {
    public abstract class RecurrencePattern {
        public int Interval { get; }
        public DateTime ReferenceDate { get; }

        public RecurrencePattern(int interval, DateTime referenceDate) {
            Interval = Guard.IsPositive(interval);
            ReferenceDate = referenceDate;
        }

        public abstract bool IsValid(DateTime date);
    }
}