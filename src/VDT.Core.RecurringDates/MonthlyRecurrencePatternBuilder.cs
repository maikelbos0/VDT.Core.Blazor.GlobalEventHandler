using System.Collections.Generic;
using System.Linq;
using System;

namespace VDT.Core.RecurringDates {
    public class MonthlyRecurrencePatternBuilder : RecurrencePatternBuilder<MonthlyRecurrencePatternBuilder> {
        public HashSet<int> DaysOfMonth { get; set; } = new HashSet<int>();

        public HashSet<(DayOfWeekInMonth, DayOfWeek)> DaysOfWeek { get; set; } = new HashSet<(DayOfWeekInMonth, DayOfWeek)>();

        public MonthlyRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        public MonthlyRecurrencePatternBuilder IncludeDaysOfMonth(params int[] days)
            => IncludeDaysOfMonth(days.AsEnumerable());

        public MonthlyRecurrencePatternBuilder IncludeDaysOfMonth(IEnumerable<int> days) {
            DaysOfMonth.UnionWith(days);
            return this;
        }

        public MonthlyRecurrencePatternBuilder IncludeDayOfWeek(DayOfWeekInMonth weekOfMonth, DayOfWeek dayOfWeek)
            => IncludeDaysOfWeek((weekOfMonth, dayOfWeek));

        public MonthlyRecurrencePatternBuilder IncludeDaysOfWeek(params (DayOfWeekInMonth, DayOfWeek)[] days)
            => IncludeDaysOfWeek(days.AsEnumerable());

        public MonthlyRecurrencePatternBuilder IncludeDaysOfWeek(IEnumerable<(DayOfWeekInMonth, DayOfWeek)> days) {
            DaysOfWeek.UnionWith(days);
            return this;
        }

        public override RecurrencePattern Build() {
            return new MonthlyRecurrencePattern(Interval, ReferenceDate ?? RecurrenceBuilder.StartDate, DaysOfMonth, DaysOfWeek);
        }
    }
}