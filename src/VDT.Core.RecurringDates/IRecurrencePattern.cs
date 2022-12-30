using System;

namespace VDT.Core.RecurringDates {
    public interface IRecurrencePattern {
        public bool IsValid(DateTime date);
    }
}