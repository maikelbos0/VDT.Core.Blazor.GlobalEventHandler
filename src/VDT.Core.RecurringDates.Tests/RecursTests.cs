using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecursTests {
        [Fact]
        public void From() {
            var result = Recurs.From(new DateTime(2022, 1, 1));

            Assert.Equal(new DateTime(2022, 1, 1), Assert.IsType<RecurrenceBuilder>(result).StartDate);
        }

        [Fact]
        public void Until() {
            var result = Recurs.Until(new DateTime(2022, 12, 31));

            Assert.Equal(new DateTime(2022, 12, 31), Assert.IsType<RecurrenceBuilder>(result).EndDate);
        }

        [Fact]
        public void StopAfter() {
            var result = Recurs.StopAfter(10);

            Assert.Equal(10, Assert.IsType<RecurrenceBuilder>(result).Occurrences);
        }

        [Fact]
        public void Daily() {
            var result = Recurs.Daily();

            Assert.Equal(1, result.Interval);
        }

        [Fact]
        public void Weekly() {
            var result = Recurs.Weekly();

            Assert.Equal(1, result.Interval);
        }

        [Fact]
        public void Monthly() {
            var result = Recurs.Monthly();

            Assert.Equal(1, result.Interval);
        }

        [Fact]
        public void WithCaching() {
            var result = Recurs.WithCaching();

            Assert.True(result.CacheDates);
        }

        [Fact]
        public void Every() {
            var result = Recurs.Every(2);

            Assert.Equal(2, result.Interval);
        }
    }
}
