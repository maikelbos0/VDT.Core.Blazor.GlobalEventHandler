using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternExtensionsTests {
        [Fact]
        public void UseCalendarWeek() {
            var pattern = new WeeklyRecurrencePattern() {
                PeriodHandling = RecurrencePatternPeriodHandling.Ongoing
            };

            Assert.Same(pattern, pattern.UseCalendarWeeks());

            Assert.Equal(RecurrencePatternPeriodHandling.Calendar, pattern.PeriodHandling);
            Assert.Equal(Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek, pattern.FirstDayOfWeek);
        }

        [Fact]
        public void UseCalendarWeek_FirstDayOfWeek() {
            var pattern = new WeeklyRecurrencePattern() {
                PeriodHandling = RecurrencePatternPeriodHandling.Ongoing
            };

            Assert.Same(pattern, pattern.UseCalendarWeeks(DayOfWeek.Tuesday));

            Assert.Equal(RecurrencePatternPeriodHandling.Calendar, pattern.PeriodHandling);
            Assert.Equal(DayOfWeek.Tuesday, pattern.FirstDayOfWeek);
        }

        [Fact]
        public void UseOngoingWeek() {
            var pattern = new WeeklyRecurrencePattern() {
                PeriodHandling = RecurrencePatternPeriodHandling.Calendar
            };

            Assert.Same(pattern, pattern.UseOngoingWeeks());

            Assert.Equal(RecurrencePatternPeriodHandling.Ongoing, pattern.PeriodHandling);
        }

        [Fact]
        public void IncludeDaysOfWeek() {
            var pattern = new WeeklyRecurrencePattern() {
                DaysOfWeek = new SortedSet<DayOfWeek>() {
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
            var pattern = new WeeklyRecurrencePattern() {
                DaysOfWeek = new SortedSet<DayOfWeek>() {
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
