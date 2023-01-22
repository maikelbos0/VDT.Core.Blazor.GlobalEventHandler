using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrenceBuilderTests {
        [Fact]
        public void From() {
            var builder = new RecurrenceBuilder();

            Assert.Same(builder, builder.From(new DateTime(2022, 1, 1)));

            Assert.Equal(new DateTime(2022, 1, 1), builder.StartDate);
        }

        [Fact]
        public void Until() {
            var builder = new RecurrenceBuilder();

            Assert.Same(builder, builder.Until(new DateTime(2022, 12, 31)));

            Assert.Equal(new DateTime(2022, 12, 31), builder.EndDate);
        }

        [Fact]
        public void StopAfter() {
            var builder = new RecurrenceBuilder();

            Assert.Same(builder, builder.StopAfter(10));

            Assert.Equal(10, builder.Occurrences);
        }

        [Fact]
        public void Daily() {
            var builder = new RecurrenceBuilder();

            var result = builder.Daily();

            Assert.Same(builder, result.RecurrenceBuilder);
            Assert.Contains(result, builder.PatternBuilders);
            Assert.Equal(1, result.Interval);
        }

        [Fact]
        public void Weekly() {
            var builder = new RecurrenceBuilder();

            var result = builder.Weekly();

            Assert.Same(builder, result.RecurrenceBuilder);
            Assert.Contains(result, builder.PatternBuilders);
            Assert.Equal(1, result.Interval);
        }

        [Fact]
        public void Monthly() {
            var builder = new RecurrenceBuilder();

            var result = builder.Monthly();

            Assert.Same(builder, result.RecurrenceBuilder);
            Assert.Contains(result, builder.PatternBuilders);
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
        public void WithDateCaching() {
            var builder = new RecurrenceBuilder();

            Assert.Same(builder, builder.WithDateCaching());

            Assert.True(builder.CacheDates);
        }

        [Fact]
        public void Build() {
            var builder = new RecurrenceBuilder() {
                StartDate = new DateTime(2022, 1, 1),
                EndDate = new DateTime(2022, 12, 31),
                Occurrences = 10
            };

            builder.PatternBuilders.Add(new DailyRecurrencePatternBuilder(builder, 3));
            builder.PatternBuilders.Add(new WeeklyRecurrencePatternBuilder(builder, 2));

            var result = builder.Build();

            Assert.Equal(builder.StartDate, result.StartDate);
            Assert.Equal(builder.EndDate, result.EndDate);
            Assert.Equal(builder.Occurrences, result.Occurrences);
            Assert.Equal(builder.PatternBuilders.Count, result.Patterns.Count);
            Assert.Contains(result.Patterns, pattern => pattern is DailyRecurrencePattern && pattern.Interval == 3);
            Assert.Contains(result.Patterns, pattern => pattern is WeeklyRecurrencePattern && pattern.Interval == 2);
        }
    }
}
