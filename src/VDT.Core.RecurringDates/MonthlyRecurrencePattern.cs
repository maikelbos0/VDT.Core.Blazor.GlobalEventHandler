using System;
using System.Collections.Generic;

namespace VDT.Core.RecurringDates {
    public class MonthlyRecurrencePattern : IRecurrencePattern {
        private readonly Recurrence recurrence;

        public RecurrencePatternPeriodHandling PeriodHandling { get; set; } = RecurrencePatternPeriodHandling.Ongoing;

        public SortedSet<int> DaysOfMonth { get; set; } = new SortedSet<int>();

        public SortedSet<(WeekOfMonth, DayOfWeek)> WeekDaysOfMonth { get; set; } = new SortedSet<(WeekOfMonth, DayOfWeek)>();

        public MonthlyRecurrencePattern(Recurrence recurrence) {
            this.recurrence = recurrence;
        }

        public DateTime? GetFirst(DateTime from) {
            throw new NotImplementedException();
        }

        public DateTime? GetNext(DateTime current) {
            throw new NotImplementedException();
        }
    }
}