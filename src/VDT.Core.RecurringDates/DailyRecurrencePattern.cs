using System;

namespace VDT.Core.RecurringDates {
    public class DailyRecurrencePattern : IRecurrencePattern {
        private readonly Recurrence recurrence;

        public RecurrencePatternWeekendHandling WeekendHandling { get; set; } = RecurrencePatternWeekendHandling.Include;

        public DailyRecurrencePattern(Recurrence recurrence) {
            this.recurrence = recurrence;
        }

        public DateTime? GetFirst(DateTime from) {
            var first = from;

            if (recurrence.Start > from) {
                first = recurrence.Start;
            }
            else if (recurrence.Interval > 1) {
                var iterations = ((from - recurrence.Start).Days + recurrence.Interval - 1) / recurrence.Interval;

                first = recurrence.Start.AddDays(iterations * recurrence.Interval);
            }

            return HandleWeekends(first);
        }

        public DateTime? GetNext(DateTime current)
            => HandleWeekends(current.AddDays(recurrence.Interval));

        private DateTime? HandleWeekends(DateTime date) {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) {
                return WeekendHandling switch {
                    RecurrencePatternWeekendHandling.Include => date,
                    RecurrencePatternWeekendHandling.Skip => SkipWeekend(date),
                    RecurrencePatternWeekendHandling.AdjustToMonday => AdjustWeekendToMonday(date),
                    _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternWeekendHandling)} '{WeekendHandling}'")
                };
            }

            return date;
        }

        private DateTime? SkipWeekend(DateTime date) {
            if (recurrence.Interval % 7 == 0) {
                return null;
            }

            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) {
                date = date.AddDays(recurrence.Interval);
            }

            return date;
        }

        private static DateTime AdjustWeekendToMonday(DateTime date)
            => date.DayOfWeek switch {
                DayOfWeek.Saturday => date.AddDays(2),
                DayOfWeek.Sunday => date.AddDays(1),
                _ => date
            };
    }
}