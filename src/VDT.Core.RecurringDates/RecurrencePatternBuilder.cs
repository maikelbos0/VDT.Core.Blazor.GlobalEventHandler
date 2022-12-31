using System;

namespace VDT.Core.RecurringDates {
    public abstract class RecurrencePatternBuilder {
        protected readonly RecurrenceBuilder recurrenceBuilder;
        protected readonly int interval;

        protected RecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) {
            this.recurrenceBuilder = recurrenceBuilder;
            this.interval = Guard.IsPositive(interval);
        }

        public RecurrenceBuilder And() => recurrenceBuilder;

        public Recurrence BuildRecurrence() => recurrenceBuilder.Build();

        public abstract RecurrencePattern Build();
    }

    public abstract class RecurrencePatternBuilder<TBuilder> : RecurrencePatternBuilder where TBuilder : RecurrencePatternBuilder<TBuilder> {
        protected DateTime? referenceDate;

        public RecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        public RecurrencePatternBuilder<TBuilder> WithReferenceDate(DateTime referenceDate) {
            this.referenceDate = referenceDate;
            return this;
        }
    }
}