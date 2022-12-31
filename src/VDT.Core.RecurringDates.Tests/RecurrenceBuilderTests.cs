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
