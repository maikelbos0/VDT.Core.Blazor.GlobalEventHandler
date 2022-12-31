using System;
using System.Collections.Generic;
using System.Threading;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePattern : RecurrencePattern, IRecurrencePattern {
        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        public HashSet<DayOfWeek> DaysOfWeek { get; set; } = new HashSet<DayOfWeek>();

        public WeeklyRecurrencePattern(int interval, DateTime referenceDate) : base(interval, referenceDate) { }

        public override bool IsValid(DateTime date) => DaysOfWeek.Contains(date.DayOfWeek) && FitsInterval(date);

        private bool FitsInterval(DateTime date) => Interval == 1 || (GetFirstDayOfWeekDate(date).Date - GetFirstDayOfWeekDate(ReferenceDate).Date).Days % (7 * Interval) == 0;

        private DateTime GetFirstDayOfWeekDate(DateTime date) => date.AddDays((FirstDayOfWeek - date.DayOfWeek - 7) % 7);
    }
}