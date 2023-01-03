using System;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Base builder for composing patterns for recurring dates
    /// </summary>
    public abstract class RecurrencePatternBuilder : IRecurrenceBuilder {
        /// <summary>
        /// Builder for date recurrences to which this pattern builder belongs
        /// </summary>
        public RecurrenceBuilder RecurrenceBuilder { get; }

        /// <summary>
        /// Interval between occurrences of the pattern to be created
        /// </summary>
        public int Interval { get; }

        /// <summary>
        /// Create a builder for composing patterns for dates
        /// </summary>
        /// <param name="recurrenceBuilder">Builder for date recurrences to which this pattern builder belongs</param>
        /// <param name="interval">Interval between occurrences of the pattern to be created</param>
        protected RecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) {
            RecurrenceBuilder = recurrenceBuilder;
            Interval = Guard.IsPositive(interval);
        }

        /// <summary>
        /// Build the pattern based on the provided specifications
        /// </summary>
        /// <returns>The composed recurring date pattern</returns>
        public abstract RecurrencePattern BuildPattern();

        /// <inheritdoc/>
        public Recurrence Build() => RecurrenceBuilder.Build();

        /// <inheritdoc/>
        public IRecurrenceBuilder From(DateTime startDate) => RecurrenceBuilder.From(startDate);

        /// <inheritdoc/>
        public IRecurrenceBuilder Until(DateTime endDate) => RecurrenceBuilder.Until(endDate);

        /// <inheritdoc/>
        public IRecurrenceBuilder StopAfter(int occurrences) => RecurrenceBuilder.StopAfter(occurrences);

        /// <inheritdoc/>
        public DailyRecurrencePatternBuilder Daily() => RecurrenceBuilder.Daily();

        /// <inheritdoc/>
        public WeeklyRecurrencePatternBuilder Weekly() => RecurrenceBuilder.Weekly();

        /// <inheritdoc/>
        public MonthlyRecurrencePatternBuilder Monthly() => RecurrenceBuilder.Monthly();

        /// <inheritdoc/>
        public RecurrencePatternBuilderStart Every(int interval) => RecurrenceBuilder.Every(interval);
    }

    /// <summary>
    /// Base builder for composing patterns for recurring dates
    /// </summary>
    /// <typeparam name="TBuilder">Builder implementation type</typeparam>
    public abstract class RecurrencePatternBuilder<TBuilder> : RecurrencePatternBuilder where TBuilder : RecurrencePatternBuilder<TBuilder> {
        /// <summary>
        /// Gets or sets the date to use as a reference when calculating dates and intervals
        /// </summary>
        public DateTime? ReferenceDate { get; set; }

        /// <summary>
        /// Create a builder for composing patterns for dates
        /// </summary>
        /// <param name="recurrenceBuilder">Builder for date recurrences to which this pattern builder belongs</param>
        /// <param name="interval">Interval between occurrences of the pattern to be created</param>
        public RecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        /// <summary>
        /// Sets the date to use as a reference when calculating dates and intervals
        /// </summary>
        /// <param name="referenceDate">Reference date</param>
        /// <returns>A reference to this recurrence pattern builder</returns>
        public RecurrencePatternBuilder<TBuilder> WithReferenceDate(DateTime referenceDate) {
            ReferenceDate = referenceDate;
            return this;
        }
    }
}