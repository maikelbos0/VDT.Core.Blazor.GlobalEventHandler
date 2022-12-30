using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class MonthlyRecurrencePattern : IRecurrencePattern {
        private readonly Recurrence recurrence;

        public HashSet<int> DaysOfMonth { get; set; } = new HashSet<int>();

        public HashSet<(DayOfWeekInMonth, DayOfWeek)> DaysOfWeek { get; set; } = new HashSet<(DayOfWeekInMonth, DayOfWeek)>();

        public MonthlyRecurrencePattern(Recurrence recurrence) {
            this.recurrence = recurrence;
        }

        internal bool IsValid(DateTime date) => FitsInterval(date) && GetDaysOfMonth(date).Contains(date.Day);

        private bool FitsInterval(DateTime date) => recurrence.Interval == 1 || (date.TotalMonths() - recurrence.Start.TotalMonths()) % recurrence.Interval == 0;

        internal HashSet<int> GetDaysOfMonth(DateTime date) {
            // TODO any corrections to last days of month could be done here, such as move to next month or move back to last day of month
            var daysInMonth = date.DaysInMonth();
            var daysOfMonth = new HashSet<int>();
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = new DateTime(date.Year, date.Month, date.DaysInMonth());

            daysOfMonth.UnionWith(DaysOfMonth.Where(dayOfMonth => dayOfMonth <= daysInMonth));
            daysOfMonth.UnionWith(DaysOfWeek
                .Where(dayOfWeek => dayOfWeek.Item1 != DayOfWeekInMonth.Last)
                .Select(dayOfWeek => firstDayOfMonth.AddDays((int)dayOfWeek.Item1 * 7 + (dayOfWeek.Item2 - firstDayOfMonth.DayOfWeek + 7) % 7).Day));
            daysOfMonth.UnionWith(DaysOfWeek
                .Where(dayOfWeek => dayOfWeek.Item1 == DayOfWeekInMonth.Last)
                .Select(dayOfWeek => lastDayOfMonth.AddDays((dayOfWeek.Item2 - lastDayOfMonth.DayOfWeek - 7) % 7).Day));

            return daysOfMonth;
        }

        public DateTime? GetFirst(DateTime from) {
            throw new NotImplementedException();
        }

        public DateTime? GetNext(DateTime current) {
            throw new NotImplementedException();
        }

        internal DateSpan GetDateSpanUntilNextDay(DateTime current, bool allowCurrent) {
            var currentDateSpan = GetCurrentDay(current);

            // TODO: FIX, take into account max number of days on base of month
            // TODO: FIX, add DaysOfWeek, started with DaysOfMonth for now
            var dateSpansInRange = DaysOfMonth
                .Select(day => new DateSpan(day < 0 ? 1 : 0, day))
                .OrderBy(dayOfMonth => dayOfMonth)
                .ToList();
            DateSpan nextDateSpan;

            dateSpansInRange.Add(dateSpansInRange[0] + new DateSpan(recurrence.Interval, 0));

            if (allowCurrent) {
                nextDateSpan = dateSpansInRange.Where(dateSpan => dateSpan >= currentDateSpan).First();
            }
            else {
                nextDateSpan = dateSpansInRange.Where(dateSpan => dateSpan > currentDateSpan).First();
            }

            return nextDateSpan - currentDateSpan;
        }

        internal DateSpan GetCurrentDay(DateTime current)
            => new DateSpan((current.TotalMonths() - recurrence.Start.TotalMonths()) % recurrence.Interval, current.Day - 1);
    }
}