using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrenceExtensionsTests {

        [Fact]
        public void StartsOn() {
            var recurrence = new Recurrence();

            recurrence.StartsOn(new DateTime(2022, 1, 1));

            Assert.Equal(new DateTime(2022, 1, 1), recurrence.Start);
        }

        [Fact]
        public void EndsOn() {
            var recurrence = new Recurrence();

            recurrence.EndsOn(new DateTime(2022, 12, 31));

            Assert.Equal(new DateTime(2022, 12, 31), recurrence.End);
        }
        [Fact]
        public void Days() {
            var recurrence = new Recurrence();

            recurrence.Days(pattern => pattern.WeekendHandling = RecurrencePatternWeekendHandling.Skip);

            Assert.Equal(RecurrencePatternWeekendHandling.Skip, Assert.IsType<DailyRecurrencePattern>(recurrence.Pattern).WeekendHandling);
        }

        [Fact]
        public void Weeks() {
            var recurrence = new Recurrence();

            recurrence.Weeks(pattern => pattern.FirstDayOfWeek = DayOfWeek.Wednesday);

            Assert.Equal(DayOfWeek.Wednesday, Assert.IsType<WeeklyRecurrencePattern>(recurrence.Pattern).FirstDayOfWeek);
        }

        [Fact]
        public void Months() {
            var recurrence = new Recurrence();

            recurrence.Months(pattern => pattern.DaysOfMonth.Add(3));

            Assert.Equal(new HashSet<int>() { 3 }, Assert.IsType<MonthlyRecurrencePattern>(recurrence.Pattern).DaysOfMonth);
        }
    }
}
