using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrenceExtensionsTests {
        [Fact]
        public void Days() {
            var recurrence = new Recurrence();

            recurrence.Days(options => options.IncludingWeekends = false);

            Assert.False(Assert.IsType<DailyRecurrenceOptions>(recurrence.Options).IncludingWeekends);
        }

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
    }
}
