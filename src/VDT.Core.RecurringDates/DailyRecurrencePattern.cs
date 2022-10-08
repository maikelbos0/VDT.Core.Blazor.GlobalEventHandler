using System;

namespace VDT.Core.RecurringDates {
    public class DailyRecurrencePattern : IRecurrencePattern {
        public RecurrencePatternWeekendHandling WeekendHandling { get; set; } = RecurrencePatternWeekendHandling.Include;

        DateTime? IRecurrencePattern.GetFirst(int interval, DateTime start, DateTime from) {
            var first = from;

            if (start > from) {
                first = start;
            }
            else if (interval > 1) {
                var iterations = ((from - start).Days + interval - 1) / interval;

                first = start.AddDays(iterations * interval);
            }

            return HandleWeekends(interval, first);
        }

        DateTime? IRecurrencePattern.GetNext(int interval, DateTime current)
            => HandleWeekends(interval, current.AddDays(interval));

        private DateTime? HandleWeekends(int interval, DateTime date) {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) {
                return WeekendHandling switch {
                    RecurrencePatternWeekendHandling.Include => date,
                    RecurrencePatternWeekendHandling.Skip => SkipWeekend(interval, date),
                    RecurrencePatternWeekendHandling.AdjustToMonday => AdjustWeekendToMonday(date),
                    _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternWeekendHandling)} '{WeekendHandling}'")
                };
            }

            return date;
        }

        private static DateTime? SkipWeekend(int interval, DateTime date) {
            if (interval % 7 == 0) {
                return null;
            }

            while (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) {
                date = date.AddDays(interval);
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