using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePatternBuilder : RecurrencePatternBuilder<WeeklyRecurrencePatternBuilder> {
        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        public HashSet<DayOfWeek> DaysOfWeek { get; set; } = new HashSet<DayOfWeek>();

        public WeeklyRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        public WeeklyRecurrencePatternBuilder UseFirstDayOfWeek(DayOfWeek firstDayOfWeek) {
            FirstDayOfWeek = firstDayOfWeek;
            return this;
        }

        public WeeklyRecurrencePatternBuilder IncludeDaysOfWeek(params DayOfWeek[] days)
            => IncludeDaysOfWeek(days.AsEnumerable());

        public WeeklyRecurrencePatternBuilder IncludeDaysOfWeek(IEnumerable<DayOfWeek> days) {
            DaysOfWeek.UnionWith(days);
            return this;
        }

        public override RecurrencePattern Build()
            => new WeeklyRecurrencePattern(Interval, ReferenceDate ?? RecurrenceBuilder.StartDate, FirstDayOfWeek, DaysOfWeek);
    }
}