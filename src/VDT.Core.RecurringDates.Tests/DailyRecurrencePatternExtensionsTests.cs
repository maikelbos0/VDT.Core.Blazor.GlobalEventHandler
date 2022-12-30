using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class DailyRecurrencePatternExtensionsTests {
        [Fact]
        public void IncludeWeekends() {
            var pattern = new DailyRecurrencePattern(1, DateTime.MinValue) {
                WeekendHandling = RecurrencePatternWeekendHandling.Include
            };

            Assert.Same(pattern, pattern.IncludeWeekends());

            Assert.Equal(RecurrencePatternWeekendHandling.Include, pattern.WeekendHandling);
        }

        [Fact]
        public void SkipWeekends() {
            var pattern = new DailyRecurrencePattern(1, DateTime.MinValue) {
                WeekendHandling = RecurrencePatternWeekendHandling.Skip
            };

            Assert.Same(pattern, pattern.SkipWeekends());

            Assert.Equal(RecurrencePatternWeekendHandling.Skip, pattern.WeekendHandling);
        }

        [Fact]
        public void AdjustWeekendsToMonday() {
            var pattern = new DailyRecurrencePattern(1, DateTime.MinValue) {
                WeekendHandling = RecurrencePatternWeekendHandling.AdjustToMonday
            };

            Assert.Same(pattern, pattern.AdjustWeekendsToMonday());

            Assert.Equal(RecurrencePatternWeekendHandling.AdjustToMonday, pattern.WeekendHandling);
        }
    }
}
