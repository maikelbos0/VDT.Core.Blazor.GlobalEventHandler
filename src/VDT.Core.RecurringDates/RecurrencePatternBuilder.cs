using System;

namespace VDT.Core.RecurringDates {
    public abstract class RecurrencePatternBuilder {
        public RecurrenceBuilder RecurrenceBuilder { get; }

        public RecurrenceBuilder And => RecurrenceBuilder;

        public int Interval { get; }

        protected RecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) {
            RecurrenceBuilder = recurrenceBuilder;
            Interval = Guard.IsPositive(interval);
        }

        public Recurrence BuildRecurrence() => RecurrenceBuilder.Build();

        public abstract RecurrencePattern Build();
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