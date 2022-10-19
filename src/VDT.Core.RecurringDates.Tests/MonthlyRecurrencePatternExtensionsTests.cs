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
        public void IncludeWeekDayOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                WeekDaysOfMonth = new SortedSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.IncludeWeekDayOfMonth(WeekOfMonth.Third, DayOfWeek.Thursday));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.First, DayOfWeek.Tuesday),
                (WeekOfMonth.Third, DayOfWeek.Thursday),
                (WeekOfMonth.Third, DayOfWeek.Friday)
            }, pattern.WeekDaysOfMonth);
        }

        [Fact]
        public void IncludeWeekDaysOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                WeekDaysOfMonth = new SortedSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.IncludeWeekDaysOfMonth((WeekOfMonth.Third, DayOfWeek.Friday), (WeekOfMonth.Third, DayOfWeek.Thursday)));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.First, DayOfWeek.Tuesday),
                (WeekOfMonth.Third, DayOfWeek.Thursday),
                (WeekOfMonth.Third, DayOfWeek.Friday)
            }, pattern.WeekDaysOfMonth);
        }

        [Fact]
        public void ExcludeWeekDayOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                WeekDaysOfMonth = new SortedSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.ExcludeWeekDayOfMonth(WeekOfMonth.Third, DayOfWeek.Friday));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.First, DayOfWeek.Tuesday)
            }, pattern.WeekDaysOfMonth);
        }

        [Fact]
        public void ExcludeWeekDaysOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                WeekDaysOfMonth = new SortedSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.ExcludeWeekDaysOfMonth((WeekOfMonth.Third, DayOfWeek.Friday), (WeekOfMonth.Third, DayOfWeek.Thursday)));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.First, DayOfWeek.Tuesday)
            }, pattern.WeekDaysOfMonth);
        }
    }
}
