using System;

namespace VDT.Core.RecurringDates {
    public interface IRecurrenceOptions {
        public DateTime? GetNext(Recurrence recurrence, DateTime current);
    }
}