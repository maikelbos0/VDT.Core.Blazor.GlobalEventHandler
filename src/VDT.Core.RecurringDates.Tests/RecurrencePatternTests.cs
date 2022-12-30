using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrencePatternTests {
        private class TestRecurrencePattern : RecurrencePattern {
            public TestRecurrencePattern(int interval, DateTime start) : base(interval, start) { }

            public override bool IsValid(DateTime date) => false;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_Throws_For_Invalid_Interval(int interval) {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TestRecurrencePattern(interval, DateTime.MinValue));
        }
    }
}
