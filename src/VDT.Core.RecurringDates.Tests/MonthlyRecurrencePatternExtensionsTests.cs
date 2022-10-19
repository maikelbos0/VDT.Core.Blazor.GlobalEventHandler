using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class MonthlyRecurrencePatternExtensionsTests {
        [Fact]
        public void UseCalendarMonths() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                PeriodHandling = RecurrencePatternPeriodHandling.Ongoing
            };

            Assert.Same(pattern, pattern.UseCalendarMonths());

            Assert.Equal(RecurrencePatternPeriodHandling.Calendar, pattern.PeriodHandling);
        }

        [Fact]
        public void UseOngoingMonths() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                PeriodHandling = RecurrencePatternPeriodHandling.Calendar
            };

            Assert.Same(pattern, pattern.UseOngoingMonths());

            Assert.Equal(RecurrencePatternPeriodHandling.Ongoing, pattern.PeriodHandling);
        }

        [Fact]
        public void IncludeDaysOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfMonth = new SortedSet<int>() { 5, 9, 17 }
            };

            Assert.Same(pattern, pattern.IncludeDaysOfMonth(9, 19));

            Assert.Equal(new[] { 5, 9, 17, 19 }, pattern.DaysOfMonth);
        }

        [Fact]
        public void ExcludeDaysOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfMonth = new SortedSet<int>() { 5, 9, 17 }
            };

            Assert.Same(pattern, pattern.ExcludeDaysOfMonth(9, 19));

            Assert.Equal(new[] { 5, 17 }, pattern.DaysOfMonth);
        }
    }
}
