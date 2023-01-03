using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Builder for composing patterns for dates that recur every week or every several weeks
    /// </summary>
    public class WeeklyRecurrencePatternBuilder : RecurrencePatternBuilder<WeeklyRecurrencePatternBuilder> {
        /// <summary>
        /// Gets or sets the first day of the week to use when calculating dates and intervals
        /// </summary>
        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        /// <summary>
        /// Gets or sets the days of the week which are valid for this recurrence pattern
        /// </summary>
        public HashSet<DayOfWeek> DaysOfWeek { get; set; } = new HashSet<DayOfWeek>();

        /// <summary>
        /// Create a builder for composing patterns for dates that recur every week or every several weeks
        /// </summary>
        /// <param name="recurrenceBuilder">Builder for date recurrences to which this pattern builder belongs</param>
        /// <param name="interval">Interval in weeks between occurrences of the pattern to be created</param>
        public WeeklyRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        /// <summary>
        /// Sets the first day of the week to use when calculating dates and intervals
        /// </summary>
        /// <param name="firstDayOfWeek">Day of the week to use as the first</param>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public WeeklyRecurrencePatternBuilder UsingFirstDayOfWeek(DayOfWeek firstDayOfWeek) {
            FirstDayOfWeek = firstDayOfWeek;
            return this;
        }

        /// <summary>
        /// Adds the given days of the week to the valid days of the week for this recurrence pattern
        /// </summary>
        /// <param name="days">Days of the week that should be added</param>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public WeeklyRecurrencePatternBuilder On(params DayOfWeek[] days)
            => On(days.AsEnumerable());

        /// <summary>
        /// Adds the given days of the week to the valid days of the week for this recurrence pattern
        /// </summary>
        /// <param name="days">Days of the week that should be added</param>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public WeeklyRecurrencePatternBuilder On(IEnumerable<DayOfWeek> days) {
            DaysOfWeek.UnionWith(days);
            return this;
        }

        /// <inheritdoc/>
        public override RecurrencePattern BuildPattern()
            => new WeeklyRecurrencePattern(Interval, ReferenceDate ?? RecurrenceBuilder.StartDate, FirstDayOfWeek, DaysOfWeek);
    }
}