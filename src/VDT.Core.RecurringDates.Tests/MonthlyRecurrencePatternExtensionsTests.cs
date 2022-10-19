using System.Collections.Generic;
using System;
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
    }
}
