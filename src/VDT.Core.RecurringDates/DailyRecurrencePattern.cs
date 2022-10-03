using System;

namespace VDT.Core.RecurringDates {
    public class DailyRecurrencePattern : IRecurrencePattern {
        public bool IncludingWeekends { get; set; } = true;

        DateTime? IRecurrencePattern.GetFirst(int interval, DateTime start, DateTime from) {
            var first = from;

            if (start > from) {
                first = start;
            }
            else if (interval > 1) {
                var iterations = (from - start).Days / interval + 1;

                first = start.AddDays(iterations * interval);
            }

            return CorrectForWeekends(interval, first);
        }

        DateTime? IRecurrencePattern.GetNext(int interval, DateTime current)
            => CorrectForWeekends(interval, current.AddDays(interval));

        private DateTime CorrectForWeekends(int interval, DateTime date) {
            if (!IncludingWeekends) {
                while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) {
                    date = date.AddDays(interval);
                }
            }

            return date;
        }
    }
}