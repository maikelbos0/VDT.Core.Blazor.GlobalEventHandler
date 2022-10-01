using System;

namespace VDT.Core.RecurringDates {
    public class NoRecurrenceOptions : IRecurrenceOptions {
        public DateTime? GetNext(Recurrence recurrence, DateTime current) => null;
    }
}