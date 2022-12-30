using System;

namespace VDT.Core.RecurringDates {
    public interface IRecurrencePattern {
        public bool IsValid(DateTime date);

        public DateTime? GetFirst(DateTime from);

        public DateTime? GetNext(DateTime current);
    }
}