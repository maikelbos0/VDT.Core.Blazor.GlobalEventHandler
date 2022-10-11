using System;

namespace VDT.Core.RecurringDates {
    public class NoRecurrencePattern : IRecurrencePattern {
        DateTime? IRecurrencePattern.GetFirst(DateTime from) => null;

        DateTime? IRecurrencePattern.GetNext(DateTime current) => null;
    }
}