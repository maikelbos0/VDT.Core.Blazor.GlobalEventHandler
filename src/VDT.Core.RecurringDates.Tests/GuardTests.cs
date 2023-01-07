using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class GuardTests {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(int.MaxValue)]
        public void IsPositive_Returns_Positive_Numbers(int value) {
            var result = Guard.IsPositive(value);

            Assert.Equal(result, value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void IsPositive_Throws_For_Negative_Numbers(int value) {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.IsPositive(value));
        }
    }
}
