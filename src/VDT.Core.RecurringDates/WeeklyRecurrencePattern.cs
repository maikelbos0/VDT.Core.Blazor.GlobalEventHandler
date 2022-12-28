using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePattern : IRecurrencePattern {
        private readonly Recurrence recurrence;

        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        public HashSet<DayOfWeek> DaysOfWeek { get; set; } = new HashSet<DayOfWeek>();

        public WeeklyRecurrencePattern(Recurrence recurrence) {
            this.recurrence = recurrence;
        }

        internal bool IsValid(DateTime date)
            => DaysOfWeek.Contains(date.DayOfWeek) && FitsInterval(date);

        private bool FitsInterval(DateTime date) => recurrence.Interval == 1 || (GetFirstDayOfWeekDate(date).Date - GetFirstDayOfWeekDate(recurrence.Start).Date).Days % (7 * recurrence.Interval) == 0;

        private DateTime GetFirstDayOfWeekDate(DateTime date) => date.AddDays((FirstDayOfWeek - date.DayOfWeek - 7) % 7);

        public DateTime? GetFirst(DateTime from) {
            if (!DaysOfWeek.Any()) {
                return null;
            }

            if (from < recurrence.Start) {
                from = recurrence.Start;
            }

            var day = GetCurrentDay(from);

            return from.AddDays(GetDaysUntilNextDay(day, true));
        }

        public DateTime? GetNext(DateTime current) {
            if (!DaysOfWeek.Any()) {
                return null;
            }

            var day = GetCurrentDay(current);

            return current.AddDays(GetDaysUntilNextDay(day, false));
        }

        internal int GetCurrentDay(DateTime current) {
            var firstDayOfWeek = (int)FirstDayOfWeek;
            var totalDays = (current - recurrence.Start).Days;
            var weekDayCorrection = (int)recurrence.Start.DayOfWeek - firstDayOfWeek;
            
            if (weekDayCorrection < 0) {
                weekDayCorrection += 7;
            }
            
            return (totalDays + weekDayCorrection) % (7 * recurrence.Interval);
        }

        internal int GetDaysUntilNextDay(int day, bool allowCurrent) {
            var firstDayOfWeek = (int)FirstDayOfWeek;
            var daysInRange = DaysOfWeek.Select(d => ((int)d - firstDayOfWeek + 7) % 7).ToList();

            if (allowCurrent) {
                return daysInRange.Where(dayOfWeek => dayOfWeek >= day).DefaultIfEmpty(daysInRange.Min() + 7 * recurrence.Interval).Min() - day;
            }
            else {
                return daysInRange.Where(dayOfWeek => dayOfWeek > day).DefaultIfEmpty(daysInRange.Min() + 7 * recurrence.Interval).Min() - day;
            }
        }
    }
}