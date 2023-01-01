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
        public void Daily() {
            var builder = new RecurrenceBuilder();

            var result = builder.Daily();

            Assert.Same(builder, result.RecurrenceBuilder);
            Assert.Equal(1, result.Interval);
        }

        [Fact]
        public void Weekly() {
            var builder = new RecurrenceBuilder();

            var result = builder.Weekly();

            Assert.Same(builder, result.RecurrenceBuilder);
            Assert.Equal(1, result.Interval);
        }

        [Fact]
        public void Monthly() {
            var builder = new RecurrenceBuilder();

            var result = builder.Monthly();

            Assert.Same(builder, result.RecurrenceBuilder);
            Assert.Equal(1, result.Interval);
        }

        [Fact]
        public void Every() {
            var builder = new RecurrenceBuilder();

            var result = builder.Every(2);

            Assert.Same(builder, result.RecurrenceBuilder);
            Assert.Equal(2, result.Interval);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Every_Throws_For_Invalid_Interval(int interval) {
            var builder = new RecurrenceBuilder();

            Assert.Throws<ArgumentOutOfRangeException>(() => builder.Every(interval));
        }

        [Fact]
        public void Build() {
            var builder = new RecurrenceBuilder() {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 12, 31)
            };

            // TODO add and test patterns
            var result = builder.Build();

            Assert.Equal(new DateTime(2022, 1, 1), result.StartDate);
            Assert.Equal(new DateTime(2022, 12, 31), result.EndDate);
        }
    }
}
