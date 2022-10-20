using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternExtensionsTests {
        [Fact]
        public void UseCalendarWeeks() {
            var pattern = new WeeklyRecurrencePattern(new Recurrence()) {
                PeriodHandling = RecurrencePatternPeriodHandling.Ongoing
            };

            Assert.Same(pattern, pattern.UseCalendarWeeks());

            Assert.Equal(RecurrencePatternPeriodHandling.Calendar, pattern.PeriodHandling);
            Assert.Equal(Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek, pattern.FirstDayOfWeek);
        }

        [Fact]
        public void UseCalendarWeeks_FirstDayOfWeek() {
            var pattern = new WeeklyRecurrencePattern(new Recurrence()) {
                PeriodHandling = RecurrencePatternPeriodHandling.Ongoing
            };

            Assert.Same(pattern, pattern.UseCalendarWeeks(DayOfWeek.Tuesday));

            Assert.Equal(RecurrencePatternPeriodHandling.Calendar, pattern.PeriodHandling);
            Assert.Equal(DayOfWeek.Tuesday, pattern.FirstDayOfWeek);
        }

        [Fact]
        public void UseOngoingWeeks() {
            var pattern = new WeeklyRecurrencePattern(new Recurrence()) {
                PeriodHandling = RecurrencePatternPeriodHandling.Calendar
            };

            Assert.Same(pattern, pattern.UseOngoingWeeks());

            Assert.Equal(RecurrencePatternPeriodHandling.Ongoing, pattern.PeriodHandling);
        }

        [Fact]
        public void IncludeDaysOfWeek() {
            var pattern = new WeeklyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<DayOfWeek>() {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday
                }
            };

            Assert.Same(pattern, pattern.IncludeDaysOfWeek(DayOfWeek.Tuesday, DayOfWeek.Friday));

            Assert.Equal(new[] {
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            }, pattern.DaysOfWeek);
        }

        [Fact]
        public void ExcludeDaysOfWeek() {
            var pattern = new WeeklyRecurrencePattern(new Recurrence()) {
                DaysOfWeek = new HashSet<DayOfWeek>() {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday
                }
            };

            Assert.Same(pattern, pattern.ExcludeDaysOfWeek(DayOfWeek.Tuesday, DayOfWeek.Friday));

            Assert.Equal(new[] {
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday
            }, pattern.DaysOfWeek);
        }
    }
}
