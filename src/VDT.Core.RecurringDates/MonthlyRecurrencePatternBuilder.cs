using System.Collections.Generic;
using System.Linq;
using System;

namespace VDT.Core.RecurringDates {
    public class MonthlyRecurrencePatternBuilder : RecurrencePatternBuilder<MonthlyRecurrencePatternBuilder> {
        public HashSet<int> DaysOfMonth { get; set; } = new HashSet<int>();

        public HashSet<(DayOfWeekInMonth, DayOfWeek)> DaysOfWeek { get; set; } = new HashSet<(DayOfWeekInMonth, DayOfWeek)>();

        public MonthlyRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        public MonthlyRecurrencePatternBuilder On(params int[] days)
            => On(days.AsEnumerable());

        public MonthlyRecurrencePatternBuilder On(IEnumerable<int> days) {
            DaysOfMonth.UnionWith(days);
            return this;
        }

        public MonthlyRecurrencePatternBuilder On(DayOfWeekInMonth weekOfMonth, DayOfWeek dayOfWeek)
            => On((weekOfMonth, dayOfWeek));

        public MonthlyRecurrencePatternBuilder On(params (DayOfWeekInMonth, DayOfWeek)[] days)
            => On(days.AsEnumerable());

        public MonthlyRecurrencePatternBuilder On(IEnumerable<(DayOfWeekInMonth, DayOfWeek)> days) {
            DaysOfWeek.UnionWith(days);
            return this;
        }

        public override RecurrencePattern BuildPattern() {
            return new MonthlyRecurrencePattern(Interval, ReferenceDate ?? RecurrenceBuilder.StartDate, DaysOfMonth, DaysOfWeek);
        }
    }
}