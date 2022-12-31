namespace VDT.Core.RecurringDates {
    public class RecurrencePatternBuilderStart {
        public RecurrenceBuilder RecurrenceBuilder { get; }

        public int Interval { get; }

        internal RecurrencePatternBuilderStart(RecurrenceBuilder recurrenceBuilder, int interval) {
            RecurrenceBuilder = recurrenceBuilder;
            Interval = Guard.IsPositive(interval);
        }

        public DailyRecurrencePatternBuilder Days() {
            var builder = new DailyRecurrencePatternBuilder(RecurrenceBuilder, Interval);
            RecurrenceBuilder.PatternBuilders.Add(builder);
            return builder;
        }   
    }
}