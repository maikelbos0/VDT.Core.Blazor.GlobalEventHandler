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

        internal (int Month, int Day) GetCurrentDay(DateTime current)
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

        internal (int Months, int Days) GetTimeUntilNextDay(int month, int day, bool allowCurrent) {
            var firstDayOfMonth = GetFirstDayOfMonth();

            // TODO: FIX, take into account max number of days on base of month
            // TODO: FIX, add DaysOfWeek, started with DaysOfMonth for now
            var daysInRange = DaysOfMonth.Select(d => d - firstDayOfMonth)
                .Select(d => new { Month = d < 0 ? 1 : 0, Day = d })
                .OrderBy(dayOfMonth => dayOfMonth.Month)
                .ThenBy(dayOfMonth => dayOfMonth.Day)
                .ToList();
            var time = new { Month = 0, Day = 0 };

            daysInRange.Add(new { Month = daysInRange[0].Month + recurrence.Interval, Day = daysInRange[0].Day });

            if (allowCurrent) {
                time = daysInRange.Where(dayOfMonth => dayOfMonth.Month > month || (dayOfMonth.Month == month && dayOfMonth.Day >= day)).First();
            }
            else {
                time = daysInRange.Where(dayOfMonth => dayOfMonth.Month > month || (dayOfMonth.Month == month && dayOfMonth.Day > day)).First();
            }

            return (time.Month - month, time.Day - day);
        }

        private int GetFirstDayOfMonth()
            => PeriodHandling switch {
                RecurrencePatternPeriodHandling.Calendar => 0,
                RecurrencePatternPeriodHandling.Ongoing => recurrence.Start.Day - 1,
                _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'")
            };
    }
}