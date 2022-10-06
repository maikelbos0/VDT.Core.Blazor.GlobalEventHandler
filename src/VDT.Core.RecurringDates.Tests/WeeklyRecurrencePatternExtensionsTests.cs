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
        public void IncludeDays() {
            var pattern = new WeeklyRecurrencePattern() {
                Days = new HashSet<DayOfWeek>() {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday
                }
            };

            Assert.Same(pattern, pattern.IncludeDays(DayOfWeek.Tuesday, DayOfWeek.Friday));

            Assert.Equal(new[] {
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            }, pattern.Days);
        }

        [Fact]
        public void ExcludeDays() {
            var pattern = new WeeklyRecurrencePattern() {
                Days = new HashSet<DayOfWeek>() {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday
                }
            };

            Assert.Same(pattern, pattern.ExcludeDays(DayOfWeek.Tuesday, DayOfWeek.Friday));

            Assert.Equal(new[] {
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday
            }, pattern.Days);
        }
    }
}
