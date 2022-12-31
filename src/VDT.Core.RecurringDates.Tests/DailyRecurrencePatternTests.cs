using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DailyRecurrencePatternTests {
        [Theory]
        [InlineData(RecurrencePatternWeekendHandling.Include, 1, "2022-12-01", "2022-12-01", true)]
        [InlineData(RecurrencePatternWeekendHandling.Include, 1, "2022-12-01", "2022-11-30", true)]
        [InlineData(RecurrencePatternWeekendHandling.Include, 1, "2022-12-01", "2022-12-03", true)]
        [InlineData(RecurrencePatternWeekendHandling.Include, 1, "2022-12-01", "2022-12-04", true)]
        [InlineData(RecurrencePatternWeekendHandling.Include, 2, "2022-12-01", "2022-12-01", true)]
        [InlineData(RecurrencePatternWeekendHandling.Include, 2, "2022-12-01", "2022-12-02", false)]
        [InlineData(RecurrencePatternWeekendHandling.Include, 2, "2022-12-01", "2022-12-03", true)]

        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2022-12-01", "2022-12-05", true)]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2022-12-01", "2022-12-06", true)]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2022-12-01", "2022-12-07", true)]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2022-12-01", "2022-12-08", true)]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2022-12-01", "2022-12-09", true)]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2022-12-01", "2022-12-10", false)]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2022-12-01", "2022-12-11", false)]

        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 7, "2022-12-02", "2022-12-12", false)]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 8, "2022-12-02", "2022-12-12", true)]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 9, "2022-12-02", "2022-12-12", true)]
        public void IsValid(RecurrencePatternWeekendHandling weekendHandling, int interval, DateTime referenceDate, DateTime date, bool expectedIsValid) {
            var pattern = new DailyRecurrencePattern(interval, referenceDate, weekendHandling);
            
            Assert.Equal(expectedIsValid, pattern.IsValid(date));
        }
    }
}
