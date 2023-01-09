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
        /// Let the <see cref="DayOfWeek.Monday"/> following any valid <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/> be a valid date 
        /// instead; subsequent days will not be corrected for this move
        /// </summary>
        AdjustToMonday,

        /// <summary>
        /// Let the <see cref="DayOfWeek.Friday"/> preceding any valid <see cref="DayOfWeek.Saturday"/> and <see cref="DayOfWeek.Sunday"/> be a valid date 
        /// instead; subsequent days will not be corrected for this move
        /// </summary>
        AdjustToFriday,

        /// <summary>
        /// Let the <see cref="DayOfWeek.Friday"/> preceding any valid <see cref="DayOfWeek.Saturday"/> and the <see cref="DayOfWeek.Monday"/> following any 
        /// valid <see cref="DayOfWeek.Sunday"/> be a valid date instead; subsequent days will not be corrected for this move
        /// </summary>
        AdjustToWeekday
    }
}