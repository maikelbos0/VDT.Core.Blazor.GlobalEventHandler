using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePattern : RecurrencePattern {
        private readonly HashSet<DayOfWeek> daysOfWeek = new();

        public DayOfWeek FirstDayOfWeek { get; }

        public IReadOnlyList<DayOfWeek> DaysOfWeek => new ReadOnlyCollection<DayOfWeek>(daysOfWeek.ToList());

        public WeeklyRecurrencePattern(int interval, DateTime referenceDate, DayOfWeek? firstDayOfWeek = null, IEnumerable<DayOfWeek>? daysOfWeek = null) : base(interval, referenceDate) {
            FirstDayOfWeek = firstDayOfWeek ?? Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            if (daysOfWeek != null && daysOfWeek.Any()) {
                this.daysOfWeek.UnionWith(daysOfWeek);
            }
            else {
                this.daysOfWeek.Add(referenceDate.DayOfWeek);
            }
        }

        public override bool IsValid(DateTime date) => daysOfWeek.Contains(date.DayOfWeek) && FitsInterval(date);

        private bool FitsInterval(DateTime date) => Interval == 1 || (GetFirstDayOfWeekDate(date).Date - GetFirstDayOfWeekDate(ReferenceDate).Date).Days % (7 * Interval) == 0;

        private DateTime GetFirstDayOfWeekDate(DateTime date) => date.AddDays((FirstDayOfWeek - date.DayOfWeek - 7) % 7);
    }
}