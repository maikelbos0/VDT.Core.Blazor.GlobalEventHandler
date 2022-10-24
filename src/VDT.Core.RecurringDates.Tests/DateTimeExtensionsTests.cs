using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DateTimeExtensionsTests {
        [Theory]
        [InlineData("2022-01-01", 2022 * 12 + 1)]
        [InlineData("2021-12-31", 2021 * 12 + 12)]
        public void TotalMonths(DateTime date, int expectedMonths) {
            Assert.Equal(expectedMonths, date.TotalMonths());
        }

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
