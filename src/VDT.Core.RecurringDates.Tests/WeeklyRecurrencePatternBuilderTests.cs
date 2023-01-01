using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace VDT.Core.RecurringDates.Tests {
    public class WeeklyRecurrencePatternBuilderTests {
        [Fact]
        public void UsingFirstDayOfWeek() {
            var builder = new WeeklyRecurrencePatternBuilder(new RecurrenceBuilder(), 1);

            Assert.Same(builder, builder.UsingFirstDayOfWeek(DayOfWeek.Tuesday));

            Assert.Equal(DayOfWeek.Tuesday, builder.FirstDayOfWeek);
        }

        [Fact]
        public void On() {
            var builder = new WeeklyRecurrencePatternBuilder(new RecurrenceBuilder(), 1) {
                DaysOfWeek = new HashSet<DayOfWeek>() {
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday
                }
            };

            Assert.Same(builder, builder.On(DayOfWeek.Tuesday, DayOfWeek.Friday));

            Assert.Equal(new[] {
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday
            }, builder.DaysOfWeek);
        }

        [Fact]
        public void Build_Defaults() {
            var recurrenceBuilder = new RecurrenceBuilder() {
                StartDate = new DateTime(2022, 1, 1)
            };
            var builder = new WeeklyRecurrencePatternBuilder(recurrenceBuilder, 1);

            var result = Assert.IsType<WeeklyRecurrencePattern>(builder.Build());

            Assert.Equal(recurrenceBuilder.StartDate, result.ReferenceDate);
            Assert.Equal(builder.Interval, result.Interval);
            Assert.Equal(Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek, result.FirstDayOfWeek);
            Assert.Equal(recurrenceBuilder.StartDate.DayOfWeek, Assert.Single(result.DaysOfWeek));
        }

        [Fact]
        public void Build_Overrides() {
            var recurrenceBuilder = new RecurrenceBuilder() {
                StartDate = new DateTime(2022, 1, 1)
            };
            var builder = new WeeklyRecurrencePatternBuilder(recurrenceBuilder, 2) {
                ReferenceDate = new DateTime(2022, 2, 1),
                FirstDayOfWeek = DayOfWeek.Wednesday,
                DaysOfWeek = new HashSet<DayOfWeek>() { DayOfWeek.Sunday, DayOfWeek.Thursday }
            };

            var result = Assert.IsType<WeeklyRecurrencePattern>(builder.Build());

            Assert.Equal(builder.ReferenceDate, result.ReferenceDate);
            Assert.Equal(builder.Interval, result.Interval);
            Assert.Equal(builder.FirstDayOfWeek, result.FirstDayOfWeek);
            Assert.Equal(builder.DaysOfWeek, result.DaysOfWeek);
        }
    }
}
