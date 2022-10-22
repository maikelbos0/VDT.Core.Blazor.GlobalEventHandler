using Xunit;
using Xunit.Sdk;

namespace VDT.Core.RecurringDates.Tests {
    public class YearMonthTests {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 11, 1)]
        [InlineData(2, 0, 2)]
        [InlineData(2, 23, 3)]
        [InlineData(2, 24, 4)]
        public void Get_Year(uint year, uint month, uint expectedYear) {
            var yearMonth = new YearMonth(year, month);

            Assert.Equal(expectedYear, yearMonth.Year);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 11, 11)]
        [InlineData(1, 23, 11)]
        [InlineData(2, 24, 0)]
        public void Get_Month(uint year, uint month, uint expectedMonth) {
            var yearMonth = new YearMonth(year, month);

            Assert.Equal(expectedMonth, yearMonth.Month);
        }

        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(3, 6, 4)]
        [InlineData(3, 11, 4)]
        public void Set_Year(uint year, uint month, uint newYear) {
            var yearMonth = new YearMonth(year, month);

            yearMonth.Year = newYear;

            Assert.Equal(newYear, yearMonth.Year);
            Assert.Equal(month, yearMonth.Month);
        }

        [Theory]
        [InlineData(0, 0, 1, 0, 1)]
        [InlineData(3, 1, 11, 3, 11)]
        [InlineData(3, 11, 1, 3, 1)]
        [InlineData(3, 11, 13, 4, 1)]
        public void Set_Month(uint year, uint month, uint newMonth, uint expectedYear, uint expectedMonth) {
            var yearMonth = new YearMonth(year, month);

            yearMonth.Month = newMonth;

            Assert.Equal(expectedYear, yearMonth.Year);
            Assert.Equal(expectedMonth, yearMonth.Month);
        }
    }
}
