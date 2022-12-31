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
        public void Build_Defaults() {
            var recurrenceBuilder = new RecurrenceBuilder() {
                StartDate = new DateTime(2022, 1, 1)
            };
            var builder = new DailyRecurrencePatternBuilder(recurrenceBuilder, 1);

            var result = Assert.IsType<DailyRecurrencePattern>(builder.Build());

            Assert.Equal(recurrenceBuilder.StartDate, result.ReferenceDate);
            Assert.Equal(1, result.Interval);
            Assert.Equal(RecurrencePatternWeekendHandling.Include, result.WeekendHandling);
        }

        [Fact]
        public void Build_Overrides() {
            var recurrenceBuilder = new RecurrenceBuilder() {
                StartDate = new DateTime(2022, 1, 1)
            };
            var builder = new DailyRecurrencePatternBuilder(recurrenceBuilder, 2) {
                ReferenceDate = new DateTime(2022, 2, 1),
                WeekendHandling = RecurrencePatternWeekendHandling.Skip
            };

            var result = Assert.IsType<DailyRecurrencePattern>(builder.Build());

            Assert.Equal(new DateTime(2022, 2, 1), result.ReferenceDate);
            Assert.Equal(2, result.Interval);
            Assert.Equal(RecurrencePatternWeekendHandling.Skip, result.WeekendHandling);
        }
    }
}
