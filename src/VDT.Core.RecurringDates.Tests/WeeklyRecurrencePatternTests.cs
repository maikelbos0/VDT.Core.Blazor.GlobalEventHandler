using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternTests {
        [Theory]
        [InlineData(DayOfWeek.Monday, 1, "2022-12-01", "2022-12-01", true, DayOfWeek.Thursday)]
        [InlineData(DayOfWeek.Thursday, 1, "2022-12-01", "2022-12-16", true, DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday, 1, "2022-12-01", "2022-12-01", true, DayOfWeek.Thursday, DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday, 1, "2022-12-01", "2022-12-01", false, DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday, 2, "2022-12-01", "2022-12-09", false, DayOfWeek.Thursday, DayOfWeek.Friday)]
        [InlineData(DayOfWeek.Monday, 2, "2022-12-01", "2022-12-16", true, DayOfWeek.Thursday, DayOfWeek.Friday)]
        public void IsValid(DayOfWeek firstDayOfWeek, int interval, DateTime start, DateTime date, bool expectedIsValid, params DayOfWeek[] daysOfWeek) {
            var pattern = new WeeklyRecurrencePattern(interval, start) {
                FirstDayOfWeek = firstDayOfWeek,
                DaysOfWeek = new HashSet<DayOfWeek>(daysOfWeek)
            };

            Assert.Equal(expectedIsValid, pattern.IsValid(date));
        }
    }
}
