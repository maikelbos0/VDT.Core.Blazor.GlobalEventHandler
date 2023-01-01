using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrencePatternBuilderTests {
        private class TestRecurrencePatternBuilder : RecurrencePatternBuilder<TestRecurrencePatternBuilder> {
            public TestRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

            public override RecurrencePattern BuildPattern() => throw new NotImplementedException();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Constructor_Throws_For_Invalid_Interval(int interval) {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TestRecurrencePatternBuilder(new RecurrenceBuilder(), interval));
        }

        [Fact]
        public void ReferenceDate() {
            var builder = new TestRecurrencePatternBuilder(new RecurrenceBuilder(), 1);

            Assert.Same(builder, builder.WithReferenceDate(new DateTime(2022, 2, 1)));

            Assert.Equal(new DateTime(2022, 2, 1), builder.ReferenceDate);
        }
    }
}
