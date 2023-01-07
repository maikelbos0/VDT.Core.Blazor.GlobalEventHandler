namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Starting point to add recurrence patterns that repeat with the provided interval
    /// </summary>
    public class RecurrencePatternBuilderStart {
        /// <summary>
        /// Builder for date recurrences to which new pattern builders will be added
        /// </summary>
        public RecurrenceBuilder RecurrenceBuilder { get; }

        /// <summary>
        /// Interval between occurrences of the pattern to be created
        /// </summary>
        public int Interval { get; }

        /// <summary>
        /// Create a starting point to add recurrence patterns that repeat with the provided interval
        /// </summary>
        /// <param name="recurrenceBuilder">Builder for date recurrences to which new pattern builders will be added</param>
        /// <param name="interval">Interval between occurrences of the pattern to be created</param>
        public RecurrencePatternBuilderStart(RecurrenceBuilder recurrenceBuilder, int interval) {
            RecurrenceBuilder = recurrenceBuilder;
            Interval = Guard.IsPositive(interval);
        }

        /// <summary>
        /// Adds a pattern to this recurrence to repeat every <see cref="Interval"/> days
        /// </summary>
        /// <returns>A builder to configure the new daily pattern</returns>
        public DailyRecurrencePatternBuilder Days() {
            var builder = new DailyRecurrencePatternBuilder(RecurrenceBuilder, Interval);
            RecurrenceBuilder.PatternBuilders.Add(builder);
            return builder;
        }

        /// <summary>
        /// Adds a pattern to this recurrence to repeat every <see cref="Interval"/> weeks
        /// </summary>
        /// <returns>A builder to configure the new weekly pattern</returns>
        public WeeklyRecurrencePatternBuilder Weeks() {
            var builder = new WeeklyRecurrencePatternBuilder(RecurrenceBuilder, Interval);
            RecurrenceBuilder.PatternBuilders.Add(builder);
            return builder;
        }

        /// <summary>
        /// Adds a pattern to this recurrence to repeat every <see cref="Interval"/> months
        /// </summary>
        /// <returns>A builder to configure the new monthly pattern</returns>
        public MonthlyRecurrencePatternBuilder Months() {
            var builder = new MonthlyRecurrencePatternBuilder(RecurrenceBuilder, Interval);
            RecurrenceBuilder.PatternBuilders.Add(builder);
            return builder;
        }
    }
}