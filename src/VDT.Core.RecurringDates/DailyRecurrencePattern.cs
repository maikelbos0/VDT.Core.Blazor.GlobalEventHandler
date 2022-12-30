using System;

namespace VDT.Core.RecurringDates {
    public class DailyRecurrencePattern : IRecurrencePattern {
        private readonly Recurrence recurrence;

        public RecurrencePatternWeekendHandling WeekendHandling { get; set; } = RecurrencePatternWeekendHandling.Include;

        public DailyRecurrencePattern(Recurrence recurrence)
            => this.recurrence = recurrence;

        public bool IsValid(DateTime date)
            => date.DayOfWeek switch {
                DayOfWeek.Monday => FitsInterval(date) || (WeekendHandling == RecurrencePatternWeekendHandling.AdjustToMonday && (FitsInterval(date.AddDays(-1)) || FitsInterval(date.AddDays(-2)))),
                DayOfWeek.Saturday => WeekendHandling == RecurrencePatternWeekendHandling.Include && FitsInterval(date),
                DayOfWeek.Sunday => WeekendHandling == RecurrencePatternWeekendHandling.Include && FitsInterval(date),
                _ => FitsInterval(date)
            };

        private bool FitsInterval(DateTime date) => recurrence.Interval == 1 || (date.Date - recurrence.Start.Date).Days % recurrence.Interval == 0;
    }
}