using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DailyRecurrencePatternBuilderTests {
        [Fact]
        public void IncludeWeekends() {
            var builder = new DailyRecurrencePatternBuilder(new RecurrenceBuilder(), 1);

            Assert.Same(builder, builder.IncludeWeekends());

            Assert.Equal(RecurrencePatternWeekendHandling.Include, builder.WeekendHandling);
        }

        [Fact]
        public void SkipWeekends() {
            var builder = new DailyRecurrencePatternBuilder(new RecurrenceBuilder(), 1);

            Assert.Same(builder, builder.SkipWeekends());

            Assert.Equal(RecurrencePatternWeekendHandling.Skip, builder.WeekendHandling);
        }

        [Fact]
        public void AdjustWeekendsToMonday() {
            var builder = new DailyRecurrencePatternBuilder(new RecurrenceBuilder(), 1);

            Assert.Same(builder, builder.AdjustWeekendsToMonday());

            Assert.Equal(RecurrencePatternWeekendHandling.AdjustToMonday, builder.WeekendHandling);
        }

        [Fact]
        public void AdjustWeekendsToFriday() {
            var builder = new DailyRecurrencePatternBuilder(new RecurrenceBuilder(), 1);

            Assert.Same(builder, builder.AdjustWeekendsToFriday());

            Assert.Equal(RecurrencePatternWeekendHandling.AdjustToFriday, builder.WeekendHandling);
        }

        [Fact]
        public void AdjustWeekendsToWeekday() {
            var builder = new DailyRecurrencePatternBuilder(new RecurrenceBuilder(), 1);

            Assert.Same(builder, builder.AdjustWeekendsToWeekday());

            Assert.Equal(RecurrencePatternWeekendHandling.AdjustToWeekday, builder.WeekendHandling);
        }

        [Fact]
        public void BuildPattern() {
            var recurrenceBuilder = new RecurrenceBuilder();
            var builder = new DailyRecurrencePatternBuilder(recurrenceBuilder, 2) {
                ReferenceDate = new DateTime(2022, 2, 1),
                WeekendHandling = RecurrencePatternWeekendHandling.Skip
            };

            var result = Assert.IsType<DailyRecurrencePattern>(builder.BuildPattern());

            Assert.Equal(builder.ReferenceDate, result.ReferenceDate);
            Assert.Equal(builder.Interval, result.Interval);
            Assert.Equal(builder.WeekendHandling, result.WeekendHandling);
        }

        [Fact]
        public void BuildPattern_Takes_StartDate_As_Default_ReferenceDate() {
            var recurrenceBuilder = new RecurrenceBuilder() { StartDate = new DateTime(2022, 2, 1) };
            var builder = new DailyRecurrencePatternBuilder(recurrenceBuilder, 2);

            var result = builder.BuildPattern();

            Assert.Equal(recurrenceBuilder.StartDate, result.ReferenceDate);
        }
    }
}
