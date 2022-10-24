using System;
using System.Collections.Generic;

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
    }
}