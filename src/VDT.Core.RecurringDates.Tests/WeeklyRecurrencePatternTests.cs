using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternTests {
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
