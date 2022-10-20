using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class MonthlyRecurrencePatternExtensionsTests {
        [Fact]
        public void UseCalendarMonths() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                PeriodHandling = RecurrencePatternPeriodHandling.Ongoing
            };

            Assert.Same(pattern, pattern.UseCalendarMonths());

            Assert.Equal(RecurrencePatternPeriodHandling.Calendar, pattern.PeriodHandling);
        }

        [Fact]
        public void UseOngoingMonths() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                PeriodHandling = RecurrencePatternPeriodHandling.Calendar
            };

            Assert.Same(pattern, pattern.UseOngoingMonths());

            Assert.Equal(RecurrencePatternPeriodHandling.Ongoing, pattern.PeriodHandling);
        }

        [Fact]
        public void IncludeDaysOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfMonth = new SortedSet<int>() { 5, 9, 17 }
            };

            Assert.Same(pattern, pattern.IncludeDaysOfMonth(9, 19));

            Assert.Equal(new[] { 5, 9, 17, 19 }, pattern.DaysOfMonth);
        }

        [Fact]
        public void ExcludeDaysOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfMonth = new SortedSet<int>() { 5, 9, 17 }
            };

            Assert.Same(pattern, pattern.ExcludeDaysOfMonth(9, 19));

            Assert.Equal(new[] { 5, 17 }, pattern.DaysOfMonth);
        }

        [Fact]
        public void IncludeDayOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new SortedSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.IncludeDayOfWeek(WeekOfMonth.Third, DayOfWeek.Thursday));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.First, DayOfWeek.Tuesday),
                (WeekOfMonth.Third, DayOfWeek.Thursday),
                (WeekOfMonth.Third, DayOfWeek.Friday)
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void IncludeDaysOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new SortedSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.IncludeDaysOfWeek((WeekOfMonth.Third, DayOfWeek.Friday), (WeekOfMonth.Third, DayOfWeek.Thursday)));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.First, DayOfWeek.Tuesday),
                (WeekOfMonth.Third, DayOfWeek.Thursday),
                (WeekOfMonth.Third, DayOfWeek.Friday)
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void ExcludeDayOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new SortedSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.ExcludeDayOfWeek(WeekOfMonth.Third, DayOfWeek.Friday));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.First, DayOfWeek.Tuesday)
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void ExcludeDaysOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new SortedSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.ExcludeDaysOfWeek((WeekOfMonth.Third, DayOfWeek.Friday), (WeekOfMonth.Third, DayOfWeek.Thursday)));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.First, DayOfWeek.Tuesday)
            }, pattern.DaysOfWeek);
        }
    }
}
