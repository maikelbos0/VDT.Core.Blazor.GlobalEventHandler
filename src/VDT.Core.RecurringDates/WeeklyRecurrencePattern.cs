using System;
using System.Collections.Generic;
using System.Threading;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePattern : IRecurrencePattern {
        private readonly Recurrence recurrence;

        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        public HashSet<DayOfWeek> DaysOfWeek { get; set; } = new HashSet<DayOfWeek>();

        public WeeklyRecurrencePattern(Recurrence recurrence) {
            this.recurrence = recurrence;
        }

        public bool IsValid(DateTime date) => DaysOfWeek.Contains(date.DayOfWeek) && FitsInterval(date);

        private bool FitsInterval(DateTime date) => recurrence.Interval == 1 || (GetFirstDayOfWeekDate(date).Date - GetFirstDayOfWeekDate(recurrence.Start).Date).Days % (7 * recurrence.Interval) == 0;

        private DateTime GetFirstDayOfWeekDate(DateTime date) => date.AddDays((FirstDayOfWeek - date.DayOfWeek - 7) % 7);
    }
}