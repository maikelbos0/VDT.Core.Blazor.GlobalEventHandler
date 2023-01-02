using System;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Builder for date recurrences
    /// </summary>
    public interface IRecurrenceBuilder {
        /// <summary>
        /// Sets the inclusive start date for a recurrence; defaults to <see cref="DateTime.MinValue"/>
        /// </summary>
        /// <param name="startDate">The inclusive start date</param>
        /// <returns>A reference to this recurrence builder</returns>
        IRecurrenceBuilder From(DateTime startDate);

        /// <summary>
        /// Sets the inclusive end date for a recurrence; defaults to <see cref="DateTime.MaxValue"/>
        /// </summary>
        /// <param name="endDate">The inclusive end date</param>
        /// <returns>A reference to this recurrence builder</returns>
        IRecurrenceBuilder Until(DateTime endDate);

        /// <summary>
        /// Sets the maximum number of occurrences for a recurrence
        /// </summary>
        /// <param name="occurrences">The maximum number of occurrences</param>
        /// <returns>A reference to this recurrence builder</returns>
        IRecurrenceBuilder StopAfter(int occurrences);

        /// <summary>
        /// Adds a pattern to this recurrence to repeat daily
        /// </summary>
        /// <returns>A builder to configure the new daily pattern</returns>
        DailyRecurrencePatternBuilder Daily();

        /// <summary>
        /// Adds a pattern to this recurrence to repeat weekly
        /// </summary>
        /// <returns>A builder to configure the new weekly pattern</returns>
        WeeklyRecurrencePatternBuilder Weekly();

        /// <summary>
        /// Adds a pattern to this recurrence to repeat monthky
        /// </summary>
        /// <returns>A builder to configure the new monthly pattern</returns>
        MonthlyRecurrencePatternBuilder Monthly();

        /// <summary>
        /// Allows for adding recurrence patterns that repeat with the provided interval
        /// </summary>
        /// <param name="interval">The interval with which to repeat the new recurrence pattern</param>
        /// <returns>A starting point to add recurrence patterns that repeat with the provided interval</returns>
        RecurrencePatternBuilderStart Every(int interval);

        /// <summary>
        /// Build the recurrence based on the provided specifications and patterns
        /// </summary>
        /// <returns>The composed recurrence</returns>
        Recurrence Build();
    }
}