using System;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Builder for composing patterns for dates that recur every day or every several days
    /// </summary>
    public class DailyRecurrencePatternBuilder : RecurrencePatternBuilder<DailyRecurrencePatternBuilder> {
        /// <summary>
        /// Gets or sets how this recurrence pattern should handle <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        public RecurrencePatternWeekendHandling? WeekendHandling { get; set; }

        /// <summary>
        /// Create a builder for composing patterns for dates that recur every day or every several days
        /// </summary>
        /// <param name="recurrenceBuilder">Builder for date recurrences to which this pattern builder belongs</param>
        /// <param name="interval">Interval in days between occurrences of the pattern to be created</param>
        public DailyRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        /// <summary>
        /// Include <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public DailyRecurrencePatternBuilder IncludeWeekends() {
            WeekendHandling = RecurrencePatternWeekendHandling.Include;
            return this;
        }

        /// <summary>
        /// Skip <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public DailyRecurrencePatternBuilder SkipWeekends() {
            WeekendHandling = RecurrencePatternWeekendHandling.Skip;
            return this;
        }

        /// <summary>
        /// Move the date to the following <see cref="DayOfWeek.Monday"/>; subsequent days will not be corrected for this move
        /// </summary>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public DailyRecurrencePatternBuilder AdjustWeekendsToMonday() {
            WeekendHandling = RecurrencePatternWeekendHandling.AdjustToMonday;
            return this;
        }

        /// <summary>
        /// Move the date to the preceding <see cref="DayOfWeek.Friday"/>; subsequent days will not be corrected for this move
        /// </summary>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public DailyRecurrencePatternBuilder AdjustWeekendsToFriday() {
            WeekendHandling = RecurrencePatternWeekendHandling.AdjustToFriday;
            return this;
        }

        /// <summary>
        /// Move any <see cref="DayOfWeek.Saturday"/> to the preceding <see cref="DayOfWeek.Friday" /> and any <see cref="DayOfWeek.Sunday"/> to the following 
        /// <see cref="DayOfWeek.Monday"/>; subsequent days will not be corrected for this move
        /// </summary>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public DailyRecurrencePatternBuilder AdjustWeekendsToWeekday() {
            WeekendHandling = RecurrencePatternWeekendHandling.AdjustToWeekday;
            return this;
        }

        /// <inheritdoc/>
        public override RecurrencePattern BuildPattern()
            => new DailyRecurrencePattern(Interval, ReferenceDate ?? RecurrenceBuilder.StartDate, WeekendHandling);
    }
}