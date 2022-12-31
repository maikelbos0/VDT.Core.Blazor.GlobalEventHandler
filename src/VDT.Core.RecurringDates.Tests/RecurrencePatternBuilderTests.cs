using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrencePatternBuilderTests {
        private class TestRecurrencePatternBuilder : RecurrencePatternBuilder<TestRecurrencePatternBuilder> {
            public TestRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

            public override RecurrencePattern Build() => throw new NotImplementedException();
        }

        [Fact]
        public void ReferenceDate() {
            var builder = new TestRecurrencePatternBuilder(new RecurrenceBuilder(), 1);

            Assert.Same(builder, builder.WithReferenceDate(new DateTime(2022, 2, 1)));

            Assert.Equal(new DateTime(2022, 2, 1), builder.ReferenceDate);
        }
    }
}
