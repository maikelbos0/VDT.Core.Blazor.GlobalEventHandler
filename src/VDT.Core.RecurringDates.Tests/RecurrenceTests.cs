using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrenceTests {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Interval_Throws_For_Invalid_Value(int interval) {
            var recurrence = new Recurrence();

            Assert.Throws<ArgumentOutOfRangeException>(() => recurrence.Interval = interval);
        }
    }
}
