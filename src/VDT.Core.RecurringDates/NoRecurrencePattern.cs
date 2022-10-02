using System;

namespace VDT.Core.RecurringDates {
    public class NoRecurrencePattern : IRecurrencePattern {
        public DateTime? GetNext(Recurrence recurrence, DateTime current) => null;
    }
}