using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrencePatternTests {
        private class TestRecurrencePattern : RecurrencePattern {
            public TestRecurrencePattern(int interval, DateTime referenceDate) : base(interval, referenceDate) { }

            public override bool IsValid(DateTime date) => throw new NotImplementedException();
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
