using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrenceTests {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Interval_Throws_For_Invalid_Value(int interval) {
            var recurrence = new Recurrence();

            Assert.Throws<ArgumentOutOfRangeException>(() => recurrence.Interval = interval);
        }

        [Fact]
        public void GetDates_No_Pattern() {
            var recurrence = new Recurrence() {
                Interval = 1,
                Start = new DateTime(2022, 1, 1),
                End = new DateTime(2022, 1, 31)
            };

            var dates = recurrence.GetDates();

            Assert.Empty(dates);
        }

        [Fact]
        public void GetDates_Range_Single_Date() {
            var recurrence = new Recurrence() {
                Interval = 1,
                Start = new DateTime(2022, 1, 1),
                End = new DateTime(2022, 1, 1)
            };

            recurrence.Pattern = new DailyRecurrencePattern(recurrence);

            var dates = recurrence.GetDates();

            Assert.Equal(new DateTime(2022, 1, 1), Assert.Single(dates));
        }

        [Fact]
        public void GetDates_Range_To() {
            var recurrence = new Recurrence() {
                Interval = 2,
                End = new DateTime(2022, 1, 31)
            };

            recurrence.Pattern = new DailyRecurrencePattern(recurrence);

            var dates = recurrence.GetDates(new DateTime(2022, 1, 1), new DateTime(2022, 1, 7));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 4),
                new DateTime(2022, 1, 6)
            }, dates);
        }

        [Fact]
        public void GetDates_Range_End() {
            var recurrence = new Recurrence() {
                Interval = 2,
                End = new DateTime(2022, 1, 7)
            };

            recurrence.Pattern = new DailyRecurrencePattern(recurrence);

            var dates = recurrence.GetDates(new DateTime(2022, 1, 1), new DateTime(2022, 1, 31));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 4),
                new DateTime(2022, 1, 6)
            }, dates);
        }

        [Fact]
        public void GetDates_Count_Start() {
            var recurrence = new Recurrence() {
                Interval = 1,
                Start = new DateTime(2022, 1, 1)
            };

            recurrence.Pattern = new DailyRecurrencePattern(recurrence);

            var dates = recurrence.GetDates(3, new DateTime(2021, 12, 1));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 3)
            }, dates);
        }

        [Fact]
        public void GetDates_Count_From() {
            var recurrence = new Recurrence() {
                Interval = 1
            };

            recurrence.Pattern = new DailyRecurrencePattern(recurrence);

            var dates = recurrence.GetDates(3, new DateTime(2022, 1, 1));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 3)
            }, dates);
        }

        [Fact]
        public void GetDates_Count_End() {
            var recurrence = new Recurrence() {
                Interval = 1,
                Start = new DateTime(2022, 1, 1),
                End = new DateTime(2022, 1, 3)
            };

            recurrence.Pattern = new DailyRecurrencePattern(recurrence);

            var dates = recurrence.GetDates(10);

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 3)
            }, dates);
        }
    }
}
