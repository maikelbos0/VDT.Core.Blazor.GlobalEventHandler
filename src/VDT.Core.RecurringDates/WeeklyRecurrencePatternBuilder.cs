using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePatternBuilder : RecurrencePatternBuilder<WeeklyRecurrencePatternBuilder> {
        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        public HashSet<DayOfWeek> DaysOfWeek { get; set; } = new HashSet<DayOfWeek>();

        public WeeklyRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

        public WeeklyRecurrencePatternBuilder UsingFirstDayOfWeek(DayOfWeek firstDayOfWeek) {
            FirstDayOfWeek = firstDayOfWeek;
            return this;
        }

        public WeeklyRecurrencePatternBuilder On(params DayOfWeek[] days)
            => On(days.AsEnumerable());

        public WeeklyRecurrencePatternBuilder On(IEnumerable<DayOfWeek> days) {
            DaysOfWeek.UnionWith(days);
            return this;
        }

        public override RecurrencePattern BuildPattern()
            => new WeeklyRecurrencePattern(Interval, ReferenceDate ?? RecurrenceBuilder.StartDate, FirstDayOfWeek, DaysOfWeek);
    }
}