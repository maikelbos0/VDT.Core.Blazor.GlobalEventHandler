using System.Collections.Generic;
using System.Linq;
using System;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Builder for composing patterns for dates that recur every month or every several months
    /// </summary>
    public class MonthlyRecurrencePatternBuilder : RecurrencePatternBuilder<MonthlyRecurrencePatternBuilder> {
        /// <summary>
        /// Gets or sets the days of the month which are valid for this recurrence pattern
        /// </summary>
        public HashSet<int> DaysOfMonth { get; set; } = new HashSet<int>();

        /// <summary>
        /// Gets or sets the ordinal days of the week (e.g. the second Thursday of the month) which are valid for this recurrence pattern
        /// </summary>
        public HashSet<(DayOfWeekInMonth, DayOfWeek)> DaysOfWeek { get; set; } = new HashSet<(DayOfWeekInMonth, DayOfWeek)>();

        /// <summary>
        /// Create a builder for composing patterns for dates that recur every month or every several months
        /// </summary>
        /// <param name="recurrenceBuilder">Builder for date recurrences to which this pattern builder belongs</param>
        /// <param name="interval">Interval in months between occurrences of the pattern to be created</param>
        public MonthlyRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        /// <summary>
        /// Adds the given days of the month to the valid days of the month for this recurrence pattern
        /// </summary>
        /// <param name="days">Days of the month that should be added</param>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public MonthlyRecurrencePatternBuilder On(params int[] days)
            => On(days.AsEnumerable());

        /// <summary>
        /// Adds the given days of the month to the valid days of the month for this recurrence pattern
        /// </summary>
        /// <param name="days">Days of the month that should be added</param>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public MonthlyRecurrencePatternBuilder On(IEnumerable<int> days) {
            DaysOfMonth.UnionWith(days);
            return this;
        }

        public MonthlyRecurrencePatternBuilder On(DayOfWeekInMonth weekOfMonth, DayOfWeek dayOfWeek)
            => On((weekOfMonth, dayOfWeek));

        public MonthlyRecurrencePatternBuilder On(params (DayOfWeekInMonth, DayOfWeek)[] days)
            => On(days.AsEnumerable());

        public MonthlyRecurrencePatternBuilder On(IEnumerable<(DayOfWeekInMonth, DayOfWeek)> days) {
            DaysOfWeek.UnionWith(days);
            return this;
        }

        /// <inheritdoc/>
        public override RecurrencePattern BuildPattern() {
            return new MonthlyRecurrencePattern(Interval, ReferenceDate ?? RecurrenceBuilder.StartDate, DaysOfMonth, DaysOfWeek);
        }
    }
}