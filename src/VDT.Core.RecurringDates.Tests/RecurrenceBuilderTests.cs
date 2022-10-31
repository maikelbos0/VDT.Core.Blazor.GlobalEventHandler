using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrenceBuilderTests {
        [Fact]
        public void StartsOn() {
            var builder = new RecurrenceBuilder();

            Assert.Same(builder, builder.StartsOn(new DateTime(2022, 1, 1)));

            Assert.Equal(new DateTime(2022, 1, 1), builder.StartDate);
        }

        [Fact]
        public void EndsOn() {
            var builder = new RecurrenceBuilder();

            Assert.Same(builder, builder.EndsOn(new DateTime(2022, 12, 31)));

            Assert.Equal(new DateTime(2022, 12, 31), builder.EndDate);
        }

        [Fact]
        public void RepeatsEvery() {
            var builder = new RecurrenceBuilder();

            Assert.Same(builder, builder.RepeatsEvery(2));

            Assert.Equal(2, builder.Interval);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void RepeatsEvery_Throws_For_Invalid_Interval(int interval) {
            var builder = new RecurrenceBuilder();

            Assert.Throws<ArgumentOutOfRangeException>(() => builder.RepeatsEvery(interval));
        }
    }
}
