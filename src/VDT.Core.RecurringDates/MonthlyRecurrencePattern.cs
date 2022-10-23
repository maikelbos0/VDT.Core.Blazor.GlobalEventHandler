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

        internal (int Month, int Day) GetCurrentDayInPattern(DateTime current) {
            switch (PeriodHandling) {
                case RecurrencePatternPeriodHandling.Calendar:
                    return (GetDifferenceInMonths(current, recurrence.Start) % recurrence.Interval, current.Day - 1);
                case RecurrencePatternPeriodHandling.Ongoing:
                    if (current.Day >= recurrence.Start.Day) {
                        return (GetDifferenceInMonths(current, recurrence.Start) % recurrence.Interval, current.Day - recurrence.Start.Day + 0);
                    }
                    else {
                        return ((GetDifferenceInMonths(current, recurrence.Start) - 1) % recurrence.Interval, current.Day - recurrence.Start.Day + current.AddMonths(-1).DaysInMonth());
                    }
                default:
                    throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'");
            }
        }

        private int GetDifferenceInMonths(DateTime minuend, DateTime subtrahend)
            => minuend.Year * 12 + minuend.Month - subtrahend.Year * 12 - subtrahend.Month;
    }
}