using System;

namespace VDT.Core.RecurringDates {
    public interface IRecurrencePattern {
        internal DateTime? GetFirst(int interval, DateTime start, DateTime from);
        internal DateTime? GetNext(int interval, DateTime current);
    }
}