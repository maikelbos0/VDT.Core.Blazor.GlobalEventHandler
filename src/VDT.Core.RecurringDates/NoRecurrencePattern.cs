using System;

namespace VDT.Core.RecurringDates {
    public class NoRecurrencePattern : IRecurrencePattern {
        DateTime? IRecurrencePattern.GetFirst(int interval, DateTime start, DateTime from) => null;

        DateTime? IRecurrencePattern.GetNext(int interval, DateTime start, DateTime current) => null;
    }
}