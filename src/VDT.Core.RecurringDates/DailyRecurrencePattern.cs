using System;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Pattern for dates that recur every day or every several days
    /// </summary>
    public class DailyRecurrencePattern : RecurrencePattern {
        /// <summary>
        /// Indicates how this recurrence pattern should handle <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        public RecurrencePatternWeekendHandling WeekendHandling { get; }

        /// <summary>
        /// Create a pattern for dates that recur every day or every several days
        /// </summary>
        /// <param name="interval">Interval in days between occurrences of this pattern</param>
        /// <param name="referenceDate">Date to use as a reference date when the interval is greater than 1</param>
        /// <param name="weekendHandling">Indicates how a recurrence pattern should handle <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/></param>
        public DailyRecurrencePattern(int interval, DateTime referenceDate, RecurrencePatternWeekendHandling? weekendHandling = null) : base(interval, referenceDate) {
            WeekendHandling = weekendHandling ?? RecurrencePatternWeekendHandling.Include;
        }

        /// <inheritdoc/>
        public override bool IsValid(DateTime date)
            => date.DayOfWeek switch {
                DayOfWeek.Monday
                    => FitsInterval(date)
                        || (WeekendHandling == RecurrencePatternWeekendHandling.AdjustToMonday && (FitsInterval(date.AddDays(-1)) || FitsInterval(date.AddDays(-2))))
                        || (WeekendHandling == RecurrencePatternWeekendHandling.AdjustToWeekday && FitsInterval(date.AddDays(-1))),
                DayOfWeek.Friday 
                    => FitsInterval(date)
                        || (WeekendHandling == RecurrencePatternWeekendHandling.AdjustToFriday && (FitsInterval(date.AddDays(1)) || FitsInterval(date.AddDays(2))))
                        || (WeekendHandling == RecurrencePatternWeekendHandling.AdjustToWeekday && FitsInterval(date.AddDays(1))),
                DayOfWeek.Saturday => WeekendHandling == RecurrencePatternWeekendHandling.Include && FitsInterval(date),
                DayOfWeek.Sunday => WeekendHandling == RecurrencePatternWeekendHandling.Include && FitsInterval(date),
                _ => FitsInterval(date)
            };

        private bool FitsInterval(DateTime date) => Interval == 1 || (date.Date - ReferenceDate.Date).Days % Interval == 0;
    }
}