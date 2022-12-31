using System;

namespace VDT.Core.RecurringDates {
    public class DailyRecurrencePattern : RecurrencePattern, IRecurrencePattern {
        public RecurrencePatternWeekendHandling WeekendHandling { get; set; } = RecurrencePatternWeekendHandling.Include;

        public DailyRecurrencePattern(int interval, DateTime referenceDate) : base(interval, referenceDate) { }

        public override bool IsValid(DateTime date)
            => date.DayOfWeek switch {
                DayOfWeek.Monday => FitsInterval(date) || (WeekendHandling == RecurrencePatternWeekendHandling.AdjustToMonday && (FitsInterval(date.AddDays(-1)) || FitsInterval(date.AddDays(-2)))),
                DayOfWeek.Saturday => WeekendHandling == RecurrencePatternWeekendHandling.Include && FitsInterval(date),
                DayOfWeek.Sunday => WeekendHandling == RecurrencePatternWeekendHandling.Include && FitsInterval(date),
                _ => FitsInterval(date)
            };

        private bool FitsInterval(DateTime date) => Interval == 1 || (date.Date - ReferenceDate.Date).Days % Interval == 0;
    }
}