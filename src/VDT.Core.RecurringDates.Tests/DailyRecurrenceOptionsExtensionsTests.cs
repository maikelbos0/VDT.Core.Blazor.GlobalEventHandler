using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DailyRecurrenceOptionsExtensionsTests {
        [Fact]
        public void IncludeWeekends() {
            var options = new DailyRecurrenceOptions() {
                IncludingWeekends = false
            };

            options.IncludeWeekends();

            Assert.True(options.IncludingWeekends);
        }

        [Fact]
        public void ExcludeWeekends() {
            var options = new DailyRecurrenceOptions() {
                IncludingWeekends = true
            };

            options.ExcludeWeekends();

            Assert.False(options.IncludingWeekends);
        }
    }
}
