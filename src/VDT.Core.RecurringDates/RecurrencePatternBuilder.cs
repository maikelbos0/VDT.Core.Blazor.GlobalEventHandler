using System;

namespace VDT.Core.RecurringDates {
    public abstract class RecurrencePatternBuilder : IRecurrenceBuilder {
        public RecurrenceBuilder RecurrenceBuilder { get; }

        public int Interval { get; }

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

    public abstract class RecurrencePatternBuilder<TBuilder> : RecurrencePatternBuilder where TBuilder : RecurrencePatternBuilder<TBuilder> {
        public DateTime? ReferenceDate { get; set; }

        public RecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        public RecurrencePatternBuilder<TBuilder> WithReferenceDate(DateTime referenceDate) {
            ReferenceDate = referenceDate;
            return this;
        }
    }
}