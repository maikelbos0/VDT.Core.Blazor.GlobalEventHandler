using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Pattern for dates that recur every month or every several months
    /// </summary>
    public class MonthlyRecurrencePattern : RecurrencePattern {
        internal readonly Dictionary<(int, int), HashSet<int>> monthCache = new();
        private readonly HashSet<int> daysOfMonth = new();
        private readonly HashSet<(DayOfWeekInMonth, DayOfWeek)> daysOfWeek = new();
        private readonly HashSet<LastDayOfMonth> lastDaysOfMonth = new();

        /// <summary>
        /// Gets the days of the month which are valid for this recurrence pattern
        /// </summary>
        public IReadOnlyList<int> DaysOfMonth => new ReadOnlyCollection<int>(daysOfMonth.ToList());

        /// <summary>
        /// Gets the ordinal days of the week (e.g. the second Thursday of the month) which are valid for this recurrence pattern
        /// </summary>
        public IReadOnlyList<(DayOfWeekInMonth, DayOfWeek)> DaysOfWeek => new ReadOnlyCollection<(DayOfWeekInMonth, DayOfWeek)>(daysOfWeek.ToList());

        /// <summary>
        /// Gets the last days of the month which are valid for this recurrence pattern
        /// </summary>
        public IReadOnlyList<LastDayOfMonth> LastDaysOfMonth => new ReadOnlyCollection<LastDayOfMonth>(lastDaysOfMonth.ToList());

        /// <summary>
        /// Create a builder for composing patterns for dates that recur every month or every several months
        /// </summary>
        /// <param name="interval">Interval in months between occurrences of the pattern to be created</param>
        /// <param name="referenceDate">Date to use to determine the reference month when the interval is greater than 1</param>
        /// <param name="daysOfMonth">Days of the month which are valid for this recurrence pattern</param>
        /// <param name="daysOfWeek">Ordinal days of the week (e.g. the second Thursday of the month) which are valid for this recurrence pattern</param>
        /// <param name="lastDaysOfMonth">Last days of the month which are valid for this recurrence pattern</param>
        public MonthlyRecurrencePattern(
            int interval, 
            DateTime referenceDate, 
            IEnumerable<int>? daysOfMonth = null, 
            IEnumerable<(DayOfWeekInMonth, DayOfWeek)>? daysOfWeek = null,
            IEnumerable<LastDayOfMonth>? lastDaysOfMonth = null
        ) : base(interval, referenceDate) {
            var addReferenceDay = true;

            if (daysOfMonth != null && daysOfMonth.Any()) {
                this.daysOfMonth.UnionWith(Guard.ArePositive(daysOfMonth));
                addReferenceDay = false;
            }
            
            if (daysOfWeek != null && daysOfWeek.Any()) {
                this.daysOfWeek.UnionWith(daysOfWeek);
                addReferenceDay = false;
            }

            if (lastDaysOfMonth != null && lastDaysOfMonth.Any()) {
                this.lastDaysOfMonth.UnionWith(lastDaysOfMonth);
                addReferenceDay = false;
            }

            if (addReferenceDay) {
                this.daysOfMonth.Add(referenceDate.Day);
            }
        }

        /// <inheritdoc/>
        public override bool IsValid(DateTime date) => FitsInterval(date) && GetDaysOfMonth(date).Contains(date.Day);

        private bool FitsInterval(DateTime date) => Interval == 1 || (date.TotalMonths() - ReferenceDate.TotalMonths()) % Interval == 0;

        internal HashSet<int> GetDaysOfMonth(DateTime date) {
            if (!monthCache.TryGetValue((date.Year, date.Month), out var allDaysOfMonth)) {
                var daysInMonth = date.DaysInMonth();
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = new DateTime(date.Year, date.Month, daysInMonth);

                allDaysOfMonth = new HashSet<int>();
                allDaysOfMonth.UnionWith(daysOfMonth.Where(dayOfMonth => dayOfMonth <= daysInMonth));
                allDaysOfMonth.UnionWith(DaysOfWeek
                    .Where(dayOfWeek => dayOfWeek.Item1 != DayOfWeekInMonth.Last)
                    .Select(dayOfWeek => firstDayOfMonth.AddDays((int)dayOfWeek.Item1 * 7 + (dayOfWeek.Item2 - firstDayOfMonth.DayOfWeek + 7) % 7).Day));
                allDaysOfMonth.UnionWith(DaysOfWeek
                    .Where(dayOfWeek => dayOfWeek.Item1 == DayOfWeekInMonth.Last)
                    .Select(dayOfWeek => lastDayOfMonth.AddDays((dayOfWeek.Item2 - lastDayOfMonth.DayOfWeek - 7) % 7).Day));
                allDaysOfMonth.UnionWith(lastDaysOfMonth.Select(lastDayOfMonth => daysInMonth - (int)lastDayOfMonth));

                monthCache[(date.Year, date.Month)] = allDaysOfMonth;
            }

            return allDaysOfMonth;
        }
    }
}