using System;
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
        public void Get_Year(int year, int month, int expectedYear) {
            var yearMonth = new YearMonth(year, month);

            Assert.Equal(expectedYear, yearMonth.Year);
        }

        [Theory]
        [InlineData(0, 0, 1)]
        [InlineData(3, 6, 4)]
        [InlineData(3, 11, 4)]
        public void Set_Year(int year, int month, int newYear) {
            var yearMonth = new YearMonth(year, month);

            yearMonth.Year = newYear;

            Assert.Equal(newYear, yearMonth.Year);
            Assert.Equal(month, yearMonth.Month);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 11, 11)]
        [InlineData(1, 23, 11)]
        [InlineData(2, 24, 0)]
        public void Get_Month(int year, int month, int expectedMonth) {
            var yearMonth = new YearMonth(year, month);

            Assert.Equal(expectedMonth, yearMonth.Month);
        }

        [Theory]
        [InlineData(0, 0, 1, 0, 1)]
        [InlineData(3, 1, 11, 3, 11)]
        [InlineData(3, 11, 1, 3, 1)]
        [InlineData(3, 11, 13, 4, 1)]
        public void Set_Month(int year, int month, int newMonth, int expectedYear, int expectedMonth) {
            var yearMonth = new YearMonth(year, month);

            yearMonth.Month = newMonth;

            Assert.Equal(expectedYear, yearMonth.Year);
            Assert.Equal(expectedMonth, yearMonth.Month);
        }

        [Fact]
        public void Deconstruct() {
            var yearMonth = new YearMonth(5, 11);

            var (year, month) = yearMonth;

            Assert.Equal(year, yearMonth.Year);
            Assert.Equal(month, yearMonth.Month);
        }

        [Fact]
        public void Convert_From_DateTime() {
            var dateTime = new DateTime(5, 11, 30);
            YearMonth yearMonth = dateTime;

            Assert.Equal(dateTime.Year, yearMonth.Year);
            Assert.Equal(dateTime.Month, yearMonth.Month);
        }
    }
}
