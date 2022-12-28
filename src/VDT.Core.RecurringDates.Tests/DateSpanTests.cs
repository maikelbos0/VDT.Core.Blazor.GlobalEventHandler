using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DateSpanTests {
        [Fact]
        public void Add() {
            var result = new DateSpan(2, 3) + new DateSpan(4, 6);

            Assert.Equal(new DateSpan(6, 9), result);
        }

        [Fact]
        public void Subtract() {
            var result = new DateSpan(5, 7) - new DateSpan(2, 3);

            Assert.Equal(new DateSpan(3, 4), result);
        }

        [Theory]
        [InlineData(1, 6, 2, 5, false)]
        [InlineData(2, 4, 2, 5, false)]
        [InlineData(0, 0, 0, 0, false)]
        [InlineData(2, 5, 2, 5, false)]
        [InlineData(2, 6, 2, 5, true)]
        [InlineData(3, 4, 2, 5, true)]
        public void GreaterThan(int aMonths, int aDays, int bMonths, int bDays, bool expectedResult) {
            var a = new DateSpan(aMonths, aDays);
            var b = new DateSpan(bMonths, bDays);

            Assert.Equal(expectedResult, a > b);
        }

        [Theory]
        [InlineData(1, 6, 2, 5, false)]
        [InlineData(2, 4, 2, 5, false)]
        [InlineData(0, 0, 0, 0, true)]
        [InlineData(2, 5, 2, 5, true)]
        [InlineData(2, 6, 2, 5, true)]
        [InlineData(3, 4, 2, 5, true)]
        public void GreaterThanOrEqual(int aMonths, int aDays, int bMonths, int bDays, bool expectedResult) {
            var a = new DateSpan(aMonths, aDays);
            var b = new DateSpan(bMonths, bDays);

            Assert.Equal(expectedResult, a >= b);
        }

        [Theory]
        [InlineData(1, 6, 2, 5, true)]
        [InlineData(2, 4, 2, 5, true)]
        [InlineData(0, 0, 0, 0, false)]
        [InlineData(2, 5, 2, 5, false)]
        [InlineData(2, 6, 2, 5, false)]
        [InlineData(3, 4, 2, 5, false)]
        public void LessThan(int aMonths, int aDays, int bMonths, int bDays, bool expectedResult) {
            var a = new DateSpan(aMonths, aDays);
            var b = new DateSpan(bMonths, bDays);

            Assert.Equal(expectedResult, a < b);
        }

        [Theory]
        [InlineData(1, 6, 2, 5, true)]
        [InlineData(2, 4, 2, 5, true)]
        [InlineData(0, 0, 0, 0, true)]
        [InlineData(2, 5, 2, 5, true)]
        [InlineData(2, 6, 2, 5, false)]
        [InlineData(3, 4, 2, 5, false)]
        public void LessThanOrEqual(int aMonths, int aDays, int bMonths, int bDays, bool expectedResult) {
            var a = new DateSpan(aMonths, aDays);
            var b = new DateSpan(bMonths, bDays);

            Assert.Equal(expectedResult, a <= b);
        }
    }
}
