using System;

namespace VDT.Core.RecurringDates {
    public class DailyRecurrencePattern : IRecurrencePattern {
        public bool IncludingWeekends { get; set; } = true;

        DateTime? IRecurrencePattern.GetFirst(int interval, DateTime start, DateTime from) {
            var first = from;

            if (interval > 1) {
                var iterations = (from - start).Days / interval + 1;

                first = start.AddDays(iterations * interval);
            }

            return CorrectForWeekends(first);
        }

        DateTime? IRecurrencePattern.GetNext(int interval, DateTime current)
            => CorrectForWeekends(current.AddDays(interval));

        private DateTime CorrectForWeekends(DateTime date) {
            if (!IncludingWeekends) {
                if (date.DayOfWeek == DayOfWeek.Saturday) {
                    date = date.AddDays(2);
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday) {
                    date = date.AddDays(1);
                }
            }

            return date;
        }
    }
}