using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class MonthlyRecurrencePattern : IRecurrencePattern {
        private readonly Recurrence recurrence;

        public RecurrencePatternPeriodHandling PeriodHandling { get; set; } = RecurrencePatternPeriodHandling.Ongoing;

        public HashSet<int> DaysOfMonth { get; set; } = new HashSet<int>();

        public HashSet<(WeekOfMonth, DayOfWeek)> DaysOfWeek { get; set; } = new HashSet<(WeekOfMonth, DayOfWeek)>();

        public MonthlyRecurrencePattern(Recurrence recurrence) {
            this.recurrence = recurrence;
        }

        public DateTime? GetFirst(DateTime from) {
            throw new NotImplementedException();
        }

        public DateTime? GetNext(DateTime current) {
            throw new NotImplementedException();
        }

        internal (int Month, int Day) GetNextDayInPattern(int currentMonth, int currentDay, bool allowCurrent) {
            var firstDayOfMonth = GetFirstDayOfMonth();

            // TODO: FIX, take into account max number of days on base of month
            // TODO: FIX, started with DaysOfMonth for now
            var daysInRange = DaysOfMonth.Select(d => d - firstDayOfMonth)
                .Select(d => (Month: d < 0 ? 1 : 0, Day: d))
                .OrderBy(dayOfMonth => dayOfMonth.Month)
                .ThenBy(dayOfMonth => dayOfMonth.Day)
                .ToList();

            daysInRange.Add((Month: daysInRange[0].Month + recurrence.Interval, Day: daysInRange[0].Day));

            if (allowCurrent) {
                return daysInRange.Where(dayOfMonth => dayOfMonth.Month > currentMonth || (dayOfMonth.Month == currentMonth && dayOfMonth.Day >= currentDay)).First();
            }
            else {
                return daysInRange.Where(dayOfMonth => dayOfMonth.Month > currentMonth || (dayOfMonth.Month == currentMonth && dayOfMonth.Day > currentDay)).First();
            }
        }

        internal (int Month, int Day) GetCurrentDayInPattern(DateTime current)
            => PeriodHandling switch {
                RecurrencePatternPeriodHandling.Calendar
                    => ((current.TotalMonths() - recurrence.Start.TotalMonths()) % recurrence.Interval, current.Day - 1),
                RecurrencePatternPeriodHandling.Ongoing when (current.Day < recurrence.Start.Day)
                    => ((current.TotalMonths() - recurrence.Start.TotalMonths() - 1) % recurrence.Interval, current.Day - recurrence.Start.Day + current.AddMonths(-1).DaysInMonth()),
                RecurrencePatternPeriodHandling.Ongoing
                    => ((current.TotalMonths() - recurrence.Start.TotalMonths()) % recurrence.Interval, current.Day - recurrence.Start.Day),
                _
                    => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'")
            };

        private int GetFirstDayOfMonth()
            => PeriodHandling switch {
                RecurrencePatternPeriodHandling.Calendar => 0,
                RecurrencePatternPeriodHandling.Ongoing => recurrence.Start.Day - 1,
                _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'")
            };
    }
}