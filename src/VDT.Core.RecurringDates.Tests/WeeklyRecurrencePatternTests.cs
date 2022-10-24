using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternTests {
        [Theory]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-10-03", "2022-01-01", "2022-10-04", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-09-26", "2022-10-05", "2022-10-08", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, "2022-09-28", "2022-01-01", "2022-10-01", DayOfWeek.Tuesday, DayOfWeek.Saturday)]

        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-03", "2022-10-10", "2022-10-18", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-05", "2022-10-10", "2022-10-18", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 2, "2022-10-05", "2022-10-19", "2022-10-22", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 3, "2022-09-28", "2022-01-01", "2022-10-15", DayOfWeek.Tuesday, DayOfWeek.Saturday)]

        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 1, "2022-09-28", "2022-01-01", "2022-10-01", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 1, "2022-09-28", "2022-10-12", "2022-10-15", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 1, "2022-09-28", "2022-10-15", "2022-10-15", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 1, "2022-09-28", "2022-10-16", "2022-10-18", DayOfWeek.Tuesday, DayOfWeek.Saturday)]

        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, 3, "2022-09-28", "2022-10-16", "2022-10-22", DayOfWeek.Tuesday, DayOfWeek.Saturday)]
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

        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Friday, 1, "2022-09-26", "2022-10-02", "2022-10-04", DayOfWeek.Tuesday, DayOfWeek.Saturday)]

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
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 0, false, 7, DayOfWeek.Monday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 6, false, 1, DayOfWeek.Monday)]

        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 0, true, 0, DayOfWeek.Monday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 1, true, 6, DayOfWeek.Monday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 6, true, 1, DayOfWeek.Monday)]

        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 1, false, 1, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 2, false, 2, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 3, false, 1, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 4, false, 1, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 5, false, 4, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]

        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 2, true, 0, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 3, true, 1, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 4, true, 0, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 5, true, 0, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, 1, 6, true, 3, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]

        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 0, false, 1, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 1, false, 1, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 2, false, 4, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 5, false, 1, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 6, false, 9, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]

        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 1, true, 0, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 2, true, 0, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 3, true, 3, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 6, true, 0, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, 2, 7, true, 8, DayOfWeek.Wednesday, DayOfWeek.Friday, DayOfWeek.Saturday)]

        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Thursday, 2, 2, false, 1, DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Thursday, 2, 3, false, 3, DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Thursday, 2, 5, false, 1, DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Thursday, 2, 6, false, 11, DayOfWeek.Tuesday, DayOfWeek.Saturday)]

        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Thursday, 2, 3, true, 0, DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Thursday, 2, 4, true, 2, DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Thursday, 2, 6, true, 0, DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Thursday, 2, 7, true, 10, DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        public void GetNextDayInPattern(RecurrencePatternPeriodHandling periodHandling, DayOfWeek firstDayOfWeek, int interval, int day, bool allowCurrent, int expectedNextDay, params DayOfWeek[] daysOfWeek) {
            var pattern = new WeeklyRecurrencePattern(new Recurrence() {
                Start = new DateTime(2022, 9, 28),
                Interval = interval
            }) {
                PeriodHandling = periodHandling,
                FirstDayOfWeek = firstDayOfWeek,
                DaysOfWeek = new HashSet<DayOfWeek>(daysOfWeek)
            };

            Assert.Equal(expectedNextDay, pattern.GetNextDayInPattern(day, allowCurrent));
        }
    }
}
