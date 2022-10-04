using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DailyRecurrencePatternTests {
        [Theory]
        [InlineData(true, 1, "2020-01-01", "2022-10-01", "2022-10-01")]
        [InlineData(true, 3, "2020-01-01", "2022-10-01", "2022-10-02")]
        [InlineData(true, 6, "2020-01-01", "2022-10-01", "2022-10-05")]
        [InlineData(false, 1, "2020-01-01", "2022-10-01", "2022-10-03")]
        [InlineData(false, 3, "2020-01-01", "2022-10-01", "2022-10-05")]
        [InlineData(true, 1, "2022-10-01", "2022-01-01", "2022-10-01")]
        public void GetFirst(bool includingWeekends, int interval, DateTime start, DateTime from, DateTime expected) {
            IRecurrencePattern pattern = new DailyRecurrencePattern() {
                IncludingWeekends = includingWeekends
            };

            Assert.Equal(expected, pattern.GetFirst(interval, start, from));
        }

        [Theory]
        [InlineData(true, 1, "2022-10-01", "2022-10-02")]
        [InlineData(true, 2, "2022-10-01", "2022-10-03")]
        [InlineData(true, 7, "2022-10-01", "2022-10-08")]
        [InlineData(false, 1, "2022-09-30", "2022-10-03")]
        [InlineData(false, 1, "2022-10-01", "2022-10-03")]
        [InlineData(false, 3, "2022-09-28", "2022-10-04")]
        [InlineData(false, 3, "2022-09-29", "2022-10-05")]
        public void GetNext_Interval(bool includingWeekends, int interval, DateTime current, DateTime expected) {
            IRecurrencePattern pattern = new DailyRecurrencePattern() {
                IncludingWeekends = includingWeekends
            };

            Assert.Equal(expected, pattern.GetNext(interval, current));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(14)]
        [InlineData(21)]
        public void GetFirst_Excludingweekends_Only_Weekends(int interval) {
            IRecurrencePattern pattern = new DailyRecurrencePattern() {
                IncludingWeekends = false                
            };

            Assert.Null(pattern.GetFirst(interval, new DateTime(2022, 10, 8), new DateTime(2022, 10, 10)));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(14)]
        [InlineData(21)]
        public void GetNext_Excludingweekends_Only_Weekends(int interval) {
            IRecurrencePattern pattern = new DailyRecurrencePattern() {
                IncludingWeekends = false
            };

            Assert.Null(pattern.GetNext(interval, new DateTime(2022, 10, 8)));
        }
    }
}
