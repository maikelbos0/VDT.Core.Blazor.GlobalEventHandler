using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternExtensionsTests {
        [Fact]
        public void UseFirstDayOfWeek() {
            var pattern = new WeeklyRecurrencePattern(new Recurrence());

            Assert.Same(pattern, pattern.UseFirstDayOfWeek(DayOfWeek.Tuesday));

            Assert.Equal(DayOfWeek.Tuesday, pattern.FirstDayOfWeek);
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
