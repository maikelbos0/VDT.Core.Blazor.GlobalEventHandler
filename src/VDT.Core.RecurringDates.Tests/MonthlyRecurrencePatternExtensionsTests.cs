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
                DaysOfWeek = new HashSet<(DayOfWeekInMonth, DayOfWeek)>() {
                    (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                    (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                    (DayOfWeekInMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.IncludeDayOfWeek(DayOfWeekInMonth.Third, DayOfWeek.Thursday));

            Assert.Equal(new[] {
                (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                (DayOfWeekInMonth.First, DayOfWeek.Monday),
                (DayOfWeekInMonth.Third, DayOfWeek.Thursday)
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void IncludeDaysOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<(DayOfWeekInMonth, DayOfWeek)>() {
                    (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                    (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                    (DayOfWeekInMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.IncludeDaysOfWeek((DayOfWeekInMonth.Third, DayOfWeek.Friday), (DayOfWeekInMonth.Third, DayOfWeek.Thursday)));

            Assert.Equal(new[] {
                (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                (DayOfWeekInMonth.First, DayOfWeek.Monday),
                (DayOfWeekInMonth.Third, DayOfWeek.Thursday)
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void ExcludeDayOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<(DayOfWeekInMonth, DayOfWeek)>() {
                    (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                    (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                    (DayOfWeekInMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.ExcludeDayOfWeek(DayOfWeekInMonth.Third, DayOfWeek.Friday));

            Assert.Equal(new[] {
                (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                (DayOfWeekInMonth.First, DayOfWeek.Monday)
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void ExcludeDaysOfWeek() {
            var pattern = new MonthlyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<(DayOfWeekInMonth, DayOfWeek)>() {
                    (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                    (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                    (DayOfWeekInMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(pattern, pattern.ExcludeDaysOfWeek((DayOfWeekInMonth.Third, DayOfWeek.Friday), (DayOfWeekInMonth.Third, DayOfWeek.Thursday)));

            Assert.Equal(new[] {
                (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                (DayOfWeekInMonth.First, DayOfWeek.Monday)
            }, pattern.DaysOfWeek);
        }
    }
}
