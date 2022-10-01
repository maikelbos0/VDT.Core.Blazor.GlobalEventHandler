using System;

namespace VDT.Core.RecurringDates {
    public class DailyRecurrenceOptions : IRecurrenceOptions {
        public bool IncludingWeekends { get; set; } = true;

        public DateTime? GetNext(Recurrence recurrence, DateTime current) {
            return current.AddDays(1).Date;
        }
    }
}