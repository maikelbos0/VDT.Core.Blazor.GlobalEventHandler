using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrencePatternBuilderStartTests {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_Throws_For_Invalid_Interval(int interval) {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RecurrencePatternBuilderStart(new RecurrenceBuilder(), interval));
        }

        [Fact]
        public void Days() {
            var recurrenceBuilder = new RecurrenceBuilder();
            var start = new RecurrencePatternBuilderStart(recurrenceBuilder, 2);

            var result = start.Days();

            Assert.Same(recurrenceBuilder, result.RecurrenceBuilder);
            Assert.Equal(2, result.Interval);
        }

        [Fact]
        public void Weeks() {
            var recurrenceBuilder = new RecurrenceBuilder();
            var start = new RecurrencePatternBuilderStart(recurrenceBuilder, 2);

            var result = start.Weeks();

            Assert.Same(recurrenceBuilder, result.RecurrenceBuilder);
            Assert.Equal(2, result.Interval);
        }
    }
}
