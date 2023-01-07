using System;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Indicates how a recurrence pattern should handle <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/>
    /// </summary>
    public enum RecurrencePatternWeekendHandling {
        /// <summary>
        /// Include <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        Include,

        /// <summary>
        /// Skip <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/>
        /// </summary>
        Skip,

        /// <summary>
        /// Move the date to the following <see cref="DayOfWeek.Monday"/>; subsequent days will not be corrected for this move
        /// </summary>
        AdjustToMonday
    }
}