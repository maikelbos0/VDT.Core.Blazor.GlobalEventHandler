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
        public void GetCurrentDay(RecurrencePatternPeriodHandling periodHandling, int interval, DateTime start, DateTime current, int expectedMonth, int expectedDay) {
            var pattern = new MonthlyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                PeriodHandling = periodHandling
            };

            var (month, day) = pattern.GetCurrentDay(current);

            Assert.Equal(expectedMonth, month);
            Assert.Equal(expectedDay, day);
        }

        [Theory]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 1, "2022-01-03", "2022-01-01", false, 0, 5, 5, 25)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 1, "2022-01-03", "2022-01-06", false, 0, 20, 5, 25)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 1, "2022-01-03", "2022-01-06", true, 0, 0, 5, 25)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, 3, "2022-01-03", "2022-02-01", false, 2, 5, 5, 25)]

        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 1, "2022-01-03", "2022-01-05", false, 0, 1, 5, 25)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 1, "2022-01-03", "2022-01-06", false, 0, 20, 5, 25)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 1, "2022-01-03", "2022-01-06", true, 0, 0, 5, 25)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 3, "2022-01-03", "2022-02-03", false, 2, 3, 5, 25)]

        // TODO FIX overflow test case
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, 3, "2022-01-10", "2022-02-03", false, -1, -1, 5, 25)]
        // TODO more tests
        public void GetTimeUntilNextDay_Only_DaysOfMonth(RecurrencePatternPeriodHandling periodHandling, int interval, DateTime start, DateTime current, bool allowCurrent, int expectedMonth, int expectedDay, params int[] daysOfMonth) {
            var pattern = new MonthlyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                PeriodHandling = periodHandling,
                DaysOfMonth = new HashSet<int>(daysOfMonth)
            };

            var (months, days) = pattern.GetTimeUntilNextDay(current, allowCurrent);

            Assert.Equal(expectedMonth, months);
            Assert.Equal(expectedDay, days);
        }
    }
}
