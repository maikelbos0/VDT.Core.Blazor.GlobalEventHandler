using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DateTimeExtensionsTests {
        [Theory]
        [InlineData("2022-01-01", 31)]
        [InlineData("2022-02-28", 28)]
        [InlineData("2024-02-01", 29)]
        [InlineData("2022-04-15", 30)]
        public void DaysInMonth(DateTime date, int expectedDaysInMonth) {
            Assert.Equal(expectedDaysInMonth, date.DaysInMonth());
        }
    }
}
