using System;

namespace VDT.Core.RecurringDates {
    public class NoRecurrencePattern : IRecurrencePattern {
        public DateTime? GetFirst(DateTime from) => null;

        public DateTime? GetNext(DateTime current) => null;
    }
}