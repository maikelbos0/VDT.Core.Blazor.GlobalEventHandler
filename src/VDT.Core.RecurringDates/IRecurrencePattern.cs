using System;

namespace VDT.Core.RecurringDates {
    public interface IRecurrencePattern {
        public DateTime? GetFirst(DateTime from);

        public DateTime? GetNext(DateTime current);
    }
}