using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Builder for date recurrences
    /// </summary>
    public class RecurrenceBuilder {
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        public int? Occurrences { get; set; }
        public List<RecurrencePatternBuilder> PatternBuilders { get; set; } = new();

        public RecurrenceBuilder From(DateTime startDate) {
            StartDate = startDate.Date;
            return this;
        }

        public RecurrenceBuilder Until(DateTime endDate) {
            EndDate = endDate.Date;
            return this;
        }

        internal RecurrenceBuilder StopAfter(int occurrences) {
            Occurrences = occurrences;
            return this;
        }

        public DailyRecurrencePatternBuilder Daily() {
            var builder = new DailyRecurrencePatternBuilder(this, 1);
            PatternBuilders.Add(builder);
            return builder;
        }

        public WeeklyRecurrencePatternBuilder Weekly() {
            var builder = new WeeklyRecurrencePatternBuilder(this, 1);
            PatternBuilders.Add(builder);
            return builder;
        }

        public MonthlyRecurrencePatternBuilder Monthly() {
            var builder = new MonthlyRecurrencePatternBuilder(this, 1);
            PatternBuilders.Add(builder);
            return builder;
        }

        public RecurrencePatternBuilderStart Every(int interval) {
            return new RecurrencePatternBuilderStart(this, interval);
        }

        public Recurrence Build() {
            return new Recurrence(StartDate, EndDate, Occurrences, PatternBuilders.Select(builder => builder.BuildPattern()));
        }
    }
}