using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class MonthlyRecurrencePatternExtensionsTests {
        [Fact]
        public void IncludeDaysOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfMonth = new HashSet<int>() { 5, 9, 17 }
            };

            Assert.Same(pattern, pattern.IncludeDaysOfMonth(9, 19));

            Assert.Equal(new[] { 5, 9, 17, 19 }, pattern.DaysOfMonth);
        }

        [Fact]
        public void ExcludeDaysOfMonth() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfMonth = new HashSet<int>() { 5, 9, 17 }
            };

            Assert.Same(pattern, pattern.ExcludeDaysOfMonth(9, 19));

            Assert.Equal(new[] { 5, 17 }, pattern.DaysOfMonth);
        }

        [Fact]
        public void IncludeDayOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.IncludeDayOfWeek(WeekOfMonth.Third, DayOfWeek.Thursday));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Tuesday),
                (WeekOfMonth.Third, DayOfWeek.Friday),
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.Third, DayOfWeek.Thursday)
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void IncludeDaysOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.IncludeDaysOfWeek((WeekOfMonth.Third, DayOfWeek.Friday), (WeekOfMonth.Third, DayOfWeek.Thursday)));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Tuesday),
                (WeekOfMonth.Third, DayOfWeek.Friday),
                (WeekOfMonth.First, DayOfWeek.Monday),
                (WeekOfMonth.Third, DayOfWeek.Thursday)
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void ExcludeDayOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.ExcludeDayOfWeek(WeekOfMonth.Third, DayOfWeek.Friday));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Tuesday),
                (WeekOfMonth.First, DayOfWeek.Monday)
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void ExcludeDaysOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<(WeekOfMonth, DayOfWeek)>() {
                    (WeekOfMonth.First, DayOfWeek.Tuesday),
                    (WeekOfMonth.Third, DayOfWeek.Friday),
                    (WeekOfMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.ExcludeDaysOfWeek((WeekOfMonth.Third, DayOfWeek.Friday), (WeekOfMonth.Third, DayOfWeek.Thursday)));

            Assert.Equal(new[] {
                (WeekOfMonth.First, DayOfWeek.Tuesday),
                (WeekOfMonth.First, DayOfWeek.Monday)
            }, pattern.DaysOfWeek);
        }
    }
}
