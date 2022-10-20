using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternTests {
        [Theory]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-03", "2022-10-10", "2022-10-18", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-04", "2022-10-10", "2022-10-18", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-05", "2022-10-10", "2022-10-18", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-05", "2022-10-18", "2022-10-18", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-05", "2022-10-19", "2022-10-22", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-10-03", "2022-01-01", "2022-10-04", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-09-26", "2022-10-05", "2022-10-08", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-09-28", "2022-01-01", "2022-10-01", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, "2022-09-28", "2022-01-01", "2022-10-08", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 3, "2022-09-21", "2022-01-01", "2022-10-08", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 2, "2022-09-28", "2022-01-01", "2022-10-01", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 2, "2022-09-28", "2022-10-12", "2022-10-15", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 2, "2022-09-28", "2022-10-15", "2022-10-15", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 2, "2022-09-28", "2022-10-16", "2022-10-18", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        public void GetFirst(RecurrencePatternPeriodHandling periodHandling, DayOfWeek firstDayOfWeek, int interval, DateTime start, DateTime from, DateTime expected, params DayOfWeek[] daysOfWeek) {
            IRecurrencePattern pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                PeriodHandling = periodHandling,
                FirstDayOfWeek = firstDayOfWeek,
                DaysOfWeek = new HashSet<DayOfWeek>(daysOfWeek)
            };

            Assert.Equal(expected, pattern.GetFirst(from));
        }

        [Theory]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-09-26", "2022-09-27", "2022-10-01", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-09-26", "2022-10-01", "2022-10-04", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-09-26", "2022-10-01", "2022-10-11", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Wednesday, 2, "2022-09-30", "2022-10-04", "2022-10-15", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Monday, 2, "2022-09-26", "2022-10-01", "2022-10-11", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Friday, 2, "2022-09-26", "2022-10-01", "2022-10-11", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Friday, 2, "2022-09-28", "2022-10-01", "2022-10-04", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        public void GetNext(RecurrencePatternPeriodHandling periodHandling, DayOfWeek firstDayOfWeek, int interval, DateTime start, DateTime current, DateTime expected, params DayOfWeek[] daysOfWeek) {
            IRecurrencePattern pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                PeriodHandling = periodHandling,
                FirstDayOfWeek = firstDayOfWeek,
                DaysOfWeek = new HashSet<DayOfWeek>(daysOfWeek)
            };

            Assert.Equal(expected, pattern.GetNext(current));
        }

        [Fact]
        public void GetFirst_No_DaysOfWeek() {
            IRecurrencePattern pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Interval = 1,
                Start = new DateTime(2022, 10, 8)
            });

            Assert.Null(pattern.GetFirst(new DateTime(2022, 10, 10)));
        }

        [Fact]
        public void GetNext_No_DaysOfWeek() {
            IRecurrencePattern pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Interval = 1,
                Start = new DateTime(2022, 10, 8)
            });

            Assert.Null(pattern.GetNext(new DateTime(2022, 10, 8)));
        }

        [Theory]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-09-26", "2022-09-27", 1)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-09-26", "2022-10-01", 5)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-09-26", "2022-10-01", 5)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Wednesday, 2, "2022-09-30", "2022-10-04", 6)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Monday, 2, "2022-09-26", "2022-10-01", 5)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Friday, 2, "2022-09-26", "2022-10-01", 5)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Friday, 2, "2022-09-28", "2022-10-01", 3)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-03", "2022-10-10", 7)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-04", "2022-10-10", 7)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-05", "2022-10-10", 7)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-05", "2022-10-18", 1)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-05", "2022-10-19", 2)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-09-26", "2022-10-05", 2)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 2, "2022-09-28", "2022-10-12", 0)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 2, "2022-09-28", "2022-10-15", 3)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 2, "2022-09-28", "2022-10-16", 4)]
        public void GetCurrentDayInPattern(RecurrencePatternPeriodHandling periodHandling, DayOfWeek firstDayOfWeek, int interval, DateTime start, DateTime current, int expected) {
            var pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Interval = interval,
                Start = start
            }) {
                PeriodHandling = periodHandling,
                FirstDayOfWeek = firstDayOfWeek
            };

            Assert.Equal(expected, pattern.GetCurrentDayInPattern(current));
        }

        [Theory]
        [InlineData(0, false, 7)]
        [InlineData(1, false, 6)]
        [InlineData(2, false, 5)]
        [InlineData(3, false, 4)]
        [InlineData(4, false, 3)]
        [InlineData(5, false, 2)]
        [InlineData(6, false, 1)]
        [InlineData(0, true, 0)]
        [InlineData(1, true, 6)]
        [InlineData(2, true, 5)]
        [InlineData(3, true, 4)]
        [InlineData(4, true, 3)]
        [InlineData(5, true, 2)]
        [InlineData(6, true, 1)]
        public void GetNextDayInPattern_Single_Interval_Single_Day(int day, bool allowCurrent, int expectedNextDay) {
            var pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Interval = 1
            }) {
                PeriodHandling = RecurrencePatternPeriodHandling.Calendar,
                FirstDayOfWeek = DayOfWeek.Monday,
                DaysOfWeek = new HashSet<DayOfWeek>() { DayOfWeek.Monday }
            };

            Assert.Equal(expectedNextDay, pattern.GetNextDayInPattern(day, allowCurrent));
        }

        [Theory]
        [InlineData(0, false, 2)]
        [InlineData(1, false, 1)]
        [InlineData(2, false, 2)]
        [InlineData(3, false, 1)]
        [InlineData(4, false, 1)]
        [InlineData(5, false, 4)]
        [InlineData(6, false, 3)]
        [InlineData(0, true, 2)]
        [InlineData(1, true, 1)]
        [InlineData(2, true, 0)]
        [InlineData(3, true, 1)]
        [InlineData(4, true, 0)]
        [InlineData(5, true, 0)]
        [InlineData(6, true, 3)]
        public void GetNextDayInPattern_Single_Interval_Multiple_Days(int day, bool allowCurrent, int expectedNextDay) {
            var pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Interval = 1
            }) {
                PeriodHandling = RecurrencePatternPeriodHandling.Calendar,
                FirstDayOfWeek = DayOfWeek.Monday,
                DaysOfWeek = new HashSet<DayOfWeek>() { DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday }
            };

            Assert.Equal(expectedNextDay, pattern.GetNextDayInPattern(day, allowCurrent));
        }

        [Theory]
        [InlineData(0, false, 1)]
        [InlineData(1, false, 1)]
        [InlineData(2, false, 4)]
        [InlineData(3, false, 3)]
        [InlineData(4, false, 2)]
        [InlineData(5, false, 1)]
        [InlineData(6, false, 9)]
        [InlineData(7, false, 8)]
        [InlineData(8, false, 7)]
        [InlineData(9, false, 6)]
        [InlineData(10, false, 5)]
        [InlineData(11, false, 4)]
        [InlineData(12, false, 3)]
        [InlineData(13, false, 2)]
        [InlineData(0, true, 1)]
        [InlineData(1, true, 0)]
        [InlineData(2, true, 0)]
        [InlineData(3, true, 3)]
        [InlineData(4, true, 2)]
        [InlineData(5, true, 1)]
        [InlineData(6, true, 0)]
        [InlineData(7, true, 8)]
        [InlineData(8, true, 7)]
        [InlineData(9, true, 6)]
        [InlineData(10, true, 5)]
        [InlineData(11, true, 4)]
        [InlineData(12, true, 3)]
        [InlineData(13, true, 2)]
        public void GetNextDayInPattern_Double_Interval_Multiple_Days(int day, bool allowCurrent, int expectedNextDay) {
            var pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Interval = 2
            }) {
                PeriodHandling = RecurrencePatternPeriodHandling.Calendar,
                FirstDayOfWeek = DayOfWeek.Thursday,
                DaysOfWeek = new HashSet<DayOfWeek>() { DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday }
            };

            Assert.Equal(expectedNextDay, pattern.GetNextDayInPattern(day, allowCurrent));
        }

        [Theory]
        [InlineData(0, false, 3)]
        [InlineData(1, false, 2)]
        [InlineData(2, false, 1)]
        [InlineData(3, false, 3)]
        [InlineData(4, false, 2)]
        [InlineData(5, false, 1)]
        [InlineData(6, false, 11)]
        [InlineData(7, false, 10)]
        [InlineData(8, false, 9)]
        [InlineData(9, false, 8)]
        [InlineData(10, false, 7)]
        [InlineData(11, false, 6)]
        [InlineData(12, false, 5)]
        [InlineData(13, false, 4)]
        [InlineData(0, true, 3)]
        [InlineData(1, true, 2)]
        [InlineData(2, true, 1)]
        [InlineData(3, true, 0)]
        [InlineData(4, true, 2)]
        [InlineData(5, true, 1)]
        [InlineData(6, true, 0)]
        [InlineData(7, true, 10)]
        [InlineData(8, true, 9)]
        [InlineData(9, true, 8)]
        [InlineData(10, true, 7)]
        [InlineData(11, true, 6)]
        [InlineData(12, true, 5)]
        [InlineData(13, true, 4)]
        public void GetNextDayInPattern_Double_Interval_Multiple_Days_Ongoing(int day, bool allowCurrent, int expectedNextDay) {
            var pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Start = new DateTime(2022, 9, 28),
                Interval = 2
            }) {
                PeriodHandling = RecurrencePatternPeriodHandling.Ongoing,
                FirstDayOfWeek = DayOfWeek.Thursday,
                DaysOfWeek = new HashSet<DayOfWeek>() { DayOfWeek.Tuesday, DayOfWeek.Saturday }
            };

            Assert.Equal(expectedNextDay, pattern.GetNextDayInPattern(day, allowCurrent));
        }
    }
}
