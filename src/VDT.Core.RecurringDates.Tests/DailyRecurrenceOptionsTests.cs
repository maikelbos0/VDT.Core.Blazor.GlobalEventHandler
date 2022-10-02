using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DailyRecurrenceOptionsTests {
        [Theory]
        [InlineData(true, "2022-09-30", "2022-10-01")]
        [InlineData(true, "2022-10-01", "2022-10-02")]
        [InlineData(false, "2022-09-30", "2022-10-03")]
        [InlineData(false, "2022-10-01", "2022-10-03")]
        public void GetNext_Weekends(bool includingWeekends, DateTime current, DateTime expected) {
            var recurrence = new Recurrence() {
                Interval = 1
            };
            var options = new DailyRecurrenceOptions() {
                IncludingWeekends = includingWeekends
            };

            Assert.Equal(expected, options.GetNext(recurrence, current));
        }
    }
}
