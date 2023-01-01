using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class MonthlyRecurrencePattern : RecurrencePattern, IRecurrencePattern {
        private readonly HashSet<int> daysOfMonth = new();
        private readonly HashSet<(DayOfWeekInMonth, DayOfWeek)> daysOfWeek = new();

        public IReadOnlyList<int> DaysOfMonth => new ReadOnlyCollection<int>(daysOfMonth.ToList());

        public IReadOnlyList<(DayOfWeekInMonth, DayOfWeek)> DaysOfWeek => new ReadOnlyCollection<(DayOfWeekInMonth, DayOfWeek)>(daysOfWeek.ToList());

        public MonthlyRecurrencePattern(int interval, DateTime referenceDate, IEnumerable<int>? daysOfMonth = null, IEnumerable<(DayOfWeekInMonth, DayOfWeek)>? daysOfWeek = null) : base(interval, referenceDate) {
            var addReferenceDay = true;

            if (daysOfMonth != null && daysOfMonth.Any()) {
                this.daysOfMonth.UnionWith(daysOfMonth);
                addReferenceDay = false;
            }
            
            if (daysOfWeek != null && daysOfWeek.Any()) {
                this.daysOfWeek.UnionWith(daysOfWeek);
                addReferenceDay = false;
            }

            if (addReferenceDay) {
                this.daysOfMonth.Add(referenceDate.Day);
            }
        }

        public override bool IsValid(DateTime date) => FitsInterval(date) && GetDaysOfMonth(date).Contains(date.Day);

        private bool FitsInterval(DateTime date) => Interval == 1 || (date.TotalMonths() - ReferenceDate.TotalMonths()) % Interval == 0;

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
    }
}