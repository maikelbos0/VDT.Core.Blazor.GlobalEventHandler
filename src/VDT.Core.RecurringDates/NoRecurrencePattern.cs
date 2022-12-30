using System;

namespace VDT.Core.RecurringDates {
    public class NoRecurrencePattern : RecurrencePattern, IRecurrencePattern {
        public NoRecurrencePattern(int interval, DateTime start) : base(interval, start) { }

        public override bool IsValid(DateTime date) => false;

        public DateTime? GetFirst(DateTime from) => null;

        public DateTime? GetNext(DateTime current) => null;
    }
}