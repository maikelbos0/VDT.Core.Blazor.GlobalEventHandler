using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DailyRecurrencePatternTests {
        [Theory]
        [InlineData(RecurrencePatternWeekendHandling.Include, 1, "2022-12-01", "2022-12-01", true)]
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
        public void IsValid(RecurrencePatternWeekendHandling weekendHandling, int interval, DateTime start, DateTime date, bool expectedIsValid) {
            var pattern = new DailyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                WeekendHandling = weekendHandling
            };
            
            Assert.Equal(expectedIsValid, pattern.IsValid(date));
        }


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
            IRecurrencePattern pattern = new DailyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                WeekendHandling = weekendHandling
            };

            Assert.Equal(expected, pattern.GetFirst(from));
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
            IRecurrencePattern pattern = new DailyRecurrencePattern(new Recurrence() {
                Interval = interval
            }) {
                WeekendHandling = weekendHandling
            };

            Assert.Equal(expected, pattern.GetNext(current));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(14)]
        [InlineData(21)]
        public void GetFirst_SkipWeekends_Only_Weekends(int interval) {
            IRecurrencePattern pattern = new DailyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = new DateTime(2022, 10, 8)
            }) {
                WeekendHandling = RecurrencePatternWeekendHandling.Skip
            };

            Assert.Null(pattern.GetFirst(new DateTime(2022, 10, 10)));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(14)]
        [InlineData(21)]
        public void GetNext_SkipWeekends_Only_Weekends(int interval) {
            IRecurrencePattern pattern = new DailyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = new DateTime(2022, 10, 8)
            }) {
                WeekendHandling = RecurrencePatternWeekendHandling.Skip
            };

            Assert.Null(pattern.GetNext(new DateTime(2022, 10, 8)));
        }
    }
}
