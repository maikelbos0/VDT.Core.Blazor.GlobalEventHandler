using System;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Starting point for creating recurrence builders
    /// </summary>
    public static class Recurs {
        /// <summary>
        /// Creates a new recurrence builder and sets the inclusive start date
        /// </summary>
        /// <param name="startDate">The inclusive start date</param>
        /// <returns>A newly created recurrence builder</returns>
        public static IRecurrenceBuilder From(DateTime startDate) => new RecurrenceBuilder().From(startDate);

        /// <summary>
        /// Creates a new recurrence builder and sets the inclusive end date
        /// </summary>
        /// <param name="endDate">The inclusive end date</param>
        /// <returns>A newly created recurrence builder</returns>
        public static IRecurrenceBuilder Until(DateTime endDate) => new RecurrenceBuilder().Until(endDate);

        /// <summary>
        /// Creates a new recurrence builder and sets the maximum number of occurrences
        /// </summary>
        /// <param name="occurrences">The maximum number of occurrences</param>
        /// <returns>A newly created recurrence builder</returns>
        public static IRecurrenceBuilder StopAfter(int occurrences) => new RecurrenceBuilder().StopAfter(occurrences);

        /// <summary>
        /// Creates a new recurrence builder and adds a pattern to it to repeat daily
        /// </summary>
        /// <returns>A builder to configure the new daily pattern</returns>
        public static DailyRecurrencePatternBuilder Daily() => new RecurrenceBuilder().Daily();

        /// <summary>
        /// Creates a new recurrence builder and adds a pattern to it to repeat weekly
        /// </summary>
        /// <returns>A builder to configure the new weekly pattern</returns>
        public static WeeklyRecurrencePatternBuilder Weekly() => new RecurrenceBuilder().Weekly();

        /// <summary>
        /// Creates a new recurrence builder and adds a pattern to it to repeat monthly
        /// </summary>
        /// <returns>A builder to configure the new monthly pattern</returns>
        public static MonthlyRecurrencePatternBuilder Monthly() => new RecurrenceBuilder().Monthly();

        /// <summary>
        /// Creates a new recurrence builder and enables caching of date validity; if you use custom patterns that can be edited the cache should not be 
        /// enabled
        /// </summary>
        /// <returns>A newly created recurrence builder</returns>
        public static RecurrenceBuilder WithCaching() => new RecurrenceBuilder().WithCaching();

        /// <summary>
        /// Creates a new recurrence builder and sets it up for adding recurrence patterns that repeat with the provided interval
        /// </summary>
        /// <param name="interval">The interval with which to repeat the new recurrence pattern</param>
        /// <returns>A starting point to add recurrence patterns that repeat with the provided interval</returns>
        public static RecurrencePatternBuilderStart Every(int interval) => new RecurrenceBuilder().Every(interval);
    }
}