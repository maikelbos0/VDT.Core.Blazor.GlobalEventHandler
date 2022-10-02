using System;

namespace VDT.Core.RecurringDates {
    public class DailyRecurrenceOptions : IRecurrenceOptions {
        public bool IncludingWeekends { get; set; } = true;

        public DateTime? GetNext(Recurrence recurrence, DateTime current) {
            var next = current.AddDays(recurrence.Interval).Date;

            if (!IncludingWeekends) {
                if (next.DayOfWeek == DayOfWeek.Saturday) {
                    next = next.AddDays(2);
                }
                else if (next.DayOfWeek == DayOfWeek.Sunday) {
                    next = next.AddDays(1);
                }
            }

            return next;
        }
    }
}