using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DailyRecurrencePatternTests {
        [Theory]
        [InlineData(RecurrencePatternWeekendHandling.Include, 1, "2020-01-01", "2022-10-01", "2022-10-01")]
        [InlineData(RecurrencePatternWeekendHandling.Include, 3, "2022-10-01", "2022-10-01", "2022-10-01")]
        [InlineData(RecurrencePatternWeekendHandling.Include, 3, "2020-01-01", "2022-10-01", "2022-10-02")]
        [InlineData(RecurrencePatternWeekendHandling.Include, 3, "2020-01-01", "2022-10-02", "2022-10-02")]
        [InlineData(RecurrencePatternWeekendHandling.Include, 6, "2022-09-23", "2022-10-01", "2022-10-05")]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2020-01-01", "2022-10-01", "2022-10-03")]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 3, "2020-01-01", "2022-10-01", "2022-10-05")]
        [InlineData(RecurrencePatternWeekendHandling.Include, 1, "2022-10-01", "2022-01-01", "2022-10-01")]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 1, "2022-09-30", "2022-01-01", "2022-09-30")]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 1, "2022-10-02", "2022-01-01", "2022-10-03")]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 1, "2022-01-01", "2022-10-01", "2022-10-03")]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 1, "2022-10-03", "2022-01-01", "2022-10-03")]
        public void GetFirst(RecurrencePatternWeekendHandling weekendHandling, int interval, DateTime start, DateTime from, DateTime expected) {
            IRecurrencePattern pattern = new DailyRecurrencePattern() {
                WeekendHandling = weekendHandling
            };

            Assert.Equal(expected, pattern.GetFirst(interval, start, from));
        }

        [Theory]
        [InlineData(RecurrencePatternWeekendHandling.Include, 1, "2022-10-01", "2022-10-02")]
        [InlineData(RecurrencePatternWeekendHandling.Include, 2, "2022-10-01", "2022-10-03")]
        [InlineData(RecurrencePatternWeekendHandling.Include, 7, "2022-10-01", "2022-10-08")]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2022-09-30", "2022-10-03")]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 1, "2022-10-01", "2022-10-03")]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 3, "2022-09-28", "2022-10-04")]
        [InlineData(RecurrencePatternWeekendHandling.Skip, 3, "2022-09-29", "2022-10-05")]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 3, "2022-09-27", "2022-09-30")]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 3, "2022-09-28", "2022-10-03")]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 3, "2022-09-29", "2022-10-03")]
        [InlineData(RecurrencePatternWeekendHandling.AdjustToMonday, 3, "2022-09-30", "2022-10-03")]
        public void GetNext(RecurrencePatternWeekendHandling weekendHandling, int interval, DateTime current, DateTime expected) {
            IRecurrencePattern pattern = new DailyRecurrencePattern() {
                WeekendHandling = weekendHandling
            };

            Assert.Equal(expected, pattern.GetNext(interval, current));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(14)]
        [InlineData(21)]
        public void GetFirst_SkipWeekends_Only_Weekends(int interval) {
            IRecurrencePattern pattern = new DailyRecurrencePattern() {
                WeekendHandling = RecurrencePatternWeekendHandling.Skip
            };

            Assert.Null(pattern.GetFirst(interval, new DateTime(2022, 10, 8), new DateTime(2022, 10, 10)));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(14)]
        [InlineData(21)]
        public void GetNext_SkipWeekends_Only_Weekends(int interval) {
            IRecurrencePattern pattern = new DailyRecurrencePattern() {
                WeekendHandling = RecurrencePatternWeekendHandling.Skip
            };

            Assert.Null(pattern.GetNext(interval, new DateTime(2022, 10, 8)));
        }
    }
}
