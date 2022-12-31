namespace VDT.Core.RecurringDates {
    public class DailyRecurrencePatternBuilder : RecurrencePatternBuilder<DailyRecurrencePatternBuilder> {
        public RecurrencePatternWeekendHandling? WeekendHandling { get; set; }

        public DailyRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        public DailyRecurrencePatternBuilder IncludeWeekends() {
            WeekendHandling = RecurrencePatternWeekendHandling.Include;
            return this;
        }

        public DailyRecurrencePatternBuilder SkipWeekends() {
            WeekendHandling = RecurrencePatternWeekendHandling.Skip;
            return this;
        }

        public DailyRecurrencePatternBuilder AdjustWeekendsToMonday() {
            WeekendHandling = RecurrencePatternWeekendHandling.AdjustToMonday;
            return this;
        }

        public override RecurrencePattern Build() {
            return new DailyRecurrencePattern(Interval, ReferenceDate ?? RecurrenceBuilder.StartDate, WeekendHandling);
        }
    }
}