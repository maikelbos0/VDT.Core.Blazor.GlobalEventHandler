using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternExtensionsTests {
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
