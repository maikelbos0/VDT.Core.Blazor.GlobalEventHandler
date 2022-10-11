using System;

namespace VDT.Core.RecurringDates {
    public interface IRecurrencePattern {
        internal DateTime? GetFirst(DateTime from);

        internal DateTime? GetNext(DateTime current);
    }
}