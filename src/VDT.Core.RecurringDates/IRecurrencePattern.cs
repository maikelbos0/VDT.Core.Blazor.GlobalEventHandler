using System;

namespace VDT.Core.RecurringDates {
    public interface IRecurrencePattern {
        public DateTime? GetNext(Recurrence recurrence, DateTime current);
    }
}