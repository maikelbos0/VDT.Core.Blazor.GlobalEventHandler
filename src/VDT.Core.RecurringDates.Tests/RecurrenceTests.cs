using System;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class RecurrenceTests {
        [Fact]
        public void GetDates_No_Pattern() {
            var recurrence = new Recurrence(new DateTime(2022, 1, 1), new DateTime(2022, 1, 31), new NoRecurrencePattern(1, new DateTime(2022, 1, 1)));

            var dates = recurrence.GetDates();

            Assert.Empty(dates);
        }

        [Fact]
        public void GetDates_Range_Single_Date() {
            var recurrence = new Recurrence(new DateTime(2022, 1, 1), new DateTime(2022, 1, 1), new DailyRecurrencePattern(1, new DateTime(2022, 1, 1)));
            
            var dates = recurrence.GetDates();

            Assert.Equal(new DateTime(2022, 1, 1), Assert.Single(dates));
        }

        [Fact]
        public void GetDates_Range_To() {
            var recurrence = new Recurrence(DateTime.MinValue, new DateTime(2022, 1, 31), new DailyRecurrencePattern(2, DateTime.MinValue));

            var dates = recurrence.GetDates(new DateTime(2022, 1, 1), new DateTime(2022, 1, 7));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 4),
                new DateTime(2022, 1, 6)
            }, dates);
        }

        [Fact]
        public void GetDates_Range_End() {
            var recurrence = new Recurrence(DateTime.MinValue, new DateTime(2022, 1, 7), new DailyRecurrencePattern(2, DateTime.MinValue));

            var dates = recurrence.GetDates(new DateTime(2022, 1, 1), new DateTime(2022, 1, 31));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 4),
                new DateTime(2022, 1, 6)
            }, dates);
        }

        [Fact]
        public void GetDates_Count_Start() {
            var recurrence = new Recurrence(new DateTime(2022, 1, 1), DateTime.MaxValue, new DailyRecurrencePattern(1, new DateTime(2022, 1, 1)));

            var dates = recurrence.GetDates(3, new DateTime(2021, 12, 1));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 3)
            }, dates);
        }

        [Fact]
        public void GetDates_Count_From() {
            var recurrence = new Recurrence(DateTime.MinValue, DateTime.MaxValue, new DailyRecurrencePattern(1, DateTime.MinValue));

            var dates = recurrence.GetDates(3, new DateTime(2022, 1, 1));

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 3)
            }, dates);
        }

        [Fact]
        public void GetDates_Count_End() {
            var recurrence = new Recurrence(new DateTime(2022, 1, 1), new DateTime(2022, 1, 3), new DailyRecurrencePattern(1, new DateTime(2022, 1, 1)));

            var dates = recurrence.GetDates(10);

            Assert.Equal(new[] {
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 2),
                new DateTime(2022, 1, 3)
            }, dates);
        }
    }
}
