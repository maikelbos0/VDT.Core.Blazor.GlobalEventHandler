using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternTests {
        [Theory]
        [InlineData(RecurrencePatternPeriodHandling.Ongoing, DayOfWeek.Sunday, "2022-10-04", 0)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Sunday, "2022-10-04", -2)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Monday, "2022-10-04", -1)]
        [InlineData(RecurrencePatternPeriodHandling.Calendar, DayOfWeek.Thursday, "2022-09-28", -6)]
        public void GetWeekStartCorrection(RecurrencePatternPeriodHandling periodHandling, DayOfWeek firstDayOfWeek, DateTime date, int expectedCorrection) {
            var pattern = new WeeklyRecurrencePattern() {
                PeriodHandling = periodHandling,
                FirstDayOfWeek = firstDayOfWeek
            };

            Assert.Equal(expectedCorrection, pattern.GetWeekStartCorrection(date));
        }

        [Fact]
        public void GetDayMap_Single_Day() {
            var pattern = new WeeklyRecurrencePattern() {
                Days = new HashSet<DayOfWeek>() {
                    DayOfWeek.Wednesday
                }
            };

            var map = pattern.GetDayMap();

            Assert.Equal(7, map[DayOfWeek.Wednesday]);
        }

        [Fact]
        public void GetDayMap_All_Days() {
            var pattern = new WeeklyRecurrencePattern() {
                Days = new HashSet<DayOfWeek>() { 
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                }
            };

            var map = pattern.GetDayMap();

            Assert.Equal(1, map[DayOfWeek.Monday]);
            Assert.Equal(1, map[DayOfWeek.Tuesday]);
            Assert.Equal(1, map[DayOfWeek.Wednesday]);
            Assert.Equal(1, map[DayOfWeek.Thursday]);
            Assert.Equal(1, map[DayOfWeek.Friday]);
            Assert.Equal(1, map[DayOfWeek.Saturday]);
            Assert.Equal(1, map[DayOfWeek.Sunday]);
        }

        [Fact]
        public void GetDayMap_Some_Days() {
            var pattern = new WeeklyRecurrencePattern() {
                Days = new HashSet<DayOfWeek>() { 
                    DayOfWeek.Monday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Saturday,
                }
            };

            var map = pattern.GetDayMap();

            Assert.Equal(3, map[DayOfWeek.Monday]);
            Assert.Equal(2, map[DayOfWeek.Thursday]);
            Assert.Equal(2, map[DayOfWeek.Saturday]);
        }
    }
}
