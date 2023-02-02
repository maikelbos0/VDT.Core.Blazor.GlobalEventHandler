using System;
using System.Threading;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternTests {
        [Fact]
        public void Constructor_Without_Days_Adds_Default_Day() {
            var startDate = new DateTime(2022, 1, 15);
            var pattern = new WeeklyRecurrencePattern(1, startDate);

            Assert.Equal(Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek, pattern.FirstDayOfWeek);
            Assert.Equal(startDate.DayOfWeek, Assert.Single(pattern.DaysOfWeek));
        }

        [Fact]
        public void Constructor_Without_FirstDayOfWeek_Sets_From_CultureInfo() {
            var startDate = new DateTime(2022, 1, 15);
            var pattern = new WeeklyRecurrencePattern(1, startDate);

            Assert.Equal(Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek, pattern.FirstDayOfWeek);
        }

        [Theory]
        [InlineData(DayOfWeek.Monday, 1, "2022-12-01", "2022-12-01", true, DayOfWeek.Thursday)]
        [InlineData(DayOfWeek.Monday, 1, "2022-12-01", "2022-11-24", true, DayOfWeek.Thursday)]
        [InlineData(DayOfWeek.Thursday, 1, "2022-12-01", "2022-12-16", true, DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday, 1, "2022-12-01", "2022-12-01", true, DayOfWeek.Thursday, DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday, 1, "2022-12-01", "2022-12-01", false, DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday, 2, "2022-12-01", "2022-12-09", false, DayOfWeek.Thursday, DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday, 2, "2022-12-01", "2022-12-16", true, DayOfWeek.Thursday, DayOfWeek.Friday)]
        public void IsValid(DayOfWeek firstDayOfWeek, int interval, DateTime referenceDate, DateTime date, bool expectedIsValid, params DayOfWeek[] daysOfWeek) {
            var pattern = new WeeklyRecurrencePattern(interval, referenceDate, firstDayOfWeek, daysOfWeek);

            Assert.Equal(expectedIsValid, pattern.IsValid(date));
        }
    }
}
