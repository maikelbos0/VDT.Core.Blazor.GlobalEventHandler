using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class MonthlyRecurrencePatternTests {
        [Theory]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 1, "2022-01-01", "2022-01-01", 0, 0)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 1, "2022-01-01", "2022-02-02", 0, 1)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 3, "2022-01-01", "2022-02-15", 1, 14)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 3, "2022-01-01", "2022-05-15", 1, 14)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 3, "2022-01-15", "2022-01-15", 0, 14)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 3, "2022-01-15", "2022-03-14", 2, 13)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 1, "2022-01-15", "2022-01-15", 0, 0)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 1, "2022-01-15", "2022-02-16", 0, 1)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 3, "2022-01-15", "2022-02-16", 1, 1)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 3, "2022-01-15", "2022-03-14", 1, 27)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 3, "2022-01-15", "2022-02-14", 0, 30)]
        public void GetCurrentDayInPattern(RecurrencePatternPeriodHandling periodHandling, int interval, DateTime start, DateTime current, int expectedMonth, int expectedDay) {
            var pattern = new MonthlyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                PeriodHandling = periodHandling
            };

            var (month, day) = pattern.GetCurrentDayInPattern(current);

            Assert.Equal(expectedMonth, month);
            Assert.Equal(expectedDay, day);
        }

        [Theory]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 1, "2022-01-01", 0, 0, false, 0, 5, 5)]
        // TODO more tests
        public void GetNextDayInPattern_Only_DaysOfMonth(RecurrencePatternPeriodHandling periodHandling, int interval, DateTime start, int currentMonth, int currentDay, bool allowCurrent, int expectedMonth, int expectedDay, params int[] daysOfMonth) {
            var pattern = new MonthlyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                PeriodHandling = periodHandling,
                DaysOfMonth = new HashSet<int>(daysOfMonth)
            };

            var (month, day) = pattern.GetNextDayInPattern(currentMonth, currentDay, allowCurrent);

            Assert.Equal(expectedMonth, month);
            Assert.Equal(expectedDay, day);
        }
    }
}
