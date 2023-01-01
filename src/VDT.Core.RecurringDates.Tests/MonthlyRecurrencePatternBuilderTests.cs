using System;
using System.Collections.Generic;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class MonthlyRecurrencePatternBuilderTests {
        [Fact]
        public void IncludeDaysOfMonth() {
            var builder = new MonthlyRecurrencePatternBuilder(new RecurrenceBuilder(), 1) {
                DaysOfMonth = new HashSet<int>() { 5, 9, 17 }
            };

            Assert.Same(builder, builder.IncludeDaysOfMonth(9, 19));

            Assert.Equal(new[] { 5, 9, 17, 19 }, builder.DaysOfMonth);
        }

        [Fact]
        public void IncludeDayOfWeek() {
            var builder = new MonthlyRecurrencePatternBuilder(new RecurrenceBuilder(), 1) {
                DaysOfWeek = new HashSet<(DayOfWeekInMonth, DayOfWeek)>() {
                    (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                    (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                    (DayOfWeekInMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(builder, builder.IncludeDayOfWeek(DayOfWeekInMonth.Third, DayOfWeek.Thursday));

            Assert.Equal(new[] {
                (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                (DayOfWeekInMonth.First, DayOfWeek.Monday),
                (DayOfWeekInMonth.Third, DayOfWeek.Thursday)
            }, builder.DaysOfWeek);
        }

        [Fact]
        public void IncludeDaysOfWeek() {
            var builder = new MonthlyRecurrencePatternBuilder(new RecurrenceBuilder(), 1) {
                DaysOfWeek = new HashSet<(DayOfWeekInMonth, DayOfWeek)>() {
                    (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                    (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                    (DayOfWeekInMonth.First, DayOfWeek.Monday)
                }
            };

            Assert.Same(builder, builder.IncludeDaysOfWeek((DayOfWeekInMonth.Third, DayOfWeek.Friday), (DayOfWeekInMonth.Third, DayOfWeek.Thursday)));

            Assert.Equal(new[] {
                (DayOfWeekInMonth.First, DayOfWeek.Tuesday),
                (DayOfWeekInMonth.Third, DayOfWeek.Friday),
                (DayOfWeekInMonth.First, DayOfWeek.Monday),
                (DayOfWeekInMonth.Third, DayOfWeek.Thursday)
            }, builder.DaysOfWeek);
        }

        [Fact]
        public void Build_Defaults() {
            var recurrenceBuilder = new RecurrenceBuilder() {
                StartDate = new DateTime(2022, 1, 1)
            };
            var builder = new MonthlyRecurrencePatternBuilder(recurrenceBuilder, 1);

            var result = Assert.IsType<MonthlyRecurrencePattern>(builder.Build());

            Assert.Equal(recurrenceBuilder.StartDate, result.ReferenceDate);
            Assert.Equal(builder.Interval, result.Interval);
            Assert.Equal(recurrenceBuilder.StartDate.Day, Assert.Single(result.DaysOfMonth));
            Assert.Empty(result.DaysOfWeek);
        }

        [Fact]
        public void Build_Overrides() {
            var recurrenceBuilder = new RecurrenceBuilder() {
                StartDate = new DateTime(2022, 1, 1)
            };
            var builder = new MonthlyRecurrencePatternBuilder(recurrenceBuilder, 2) {
                ReferenceDate = new DateTime(2022, 2, 1),
                DaysOfMonth = new HashSet<int>() { 3, 5, 20 },
                DaysOfWeek = new HashSet<(DayOfWeekInMonth, DayOfWeek)>() { (DayOfWeekInMonth.First, DayOfWeek.Sunday), (DayOfWeekInMonth.Third, DayOfWeek.Thursday) }
            };

            var result = Assert.IsType<MonthlyRecurrencePattern>(builder.Build());

            Assert.Equal(builder.ReferenceDate, result.ReferenceDate);
            Assert.Equal(builder.Interval, result.Interval);
            Assert.Equal(builder.DaysOfMonth, result.DaysOfMonth);
            Assert.Equal(builder.DaysOfWeek, result.DaysOfWeek);
        }
    }
}
