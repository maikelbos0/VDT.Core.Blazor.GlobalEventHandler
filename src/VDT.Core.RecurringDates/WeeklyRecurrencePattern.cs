using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePattern : IRecurrencePattern {
        private readonly Recurrence recurrence;

        public RecurrencePatternPeriodHandling PeriodHandling { get; set; } = RecurrencePatternPeriodHandling.Ongoing;

        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        public HashSet<DayOfWeek> DaysOfWeek { get; set; } = new HashSet<DayOfWeek>();

        public WeeklyRecurrencePattern(Recurrence recurrence) {
            this.recurrence = recurrence;
        }

        public DateTime? GetFirst(DateTime from) {
            if (!DaysOfWeek.Any()) {
                return null;
            }

            if (from < recurrence.Start) {
                from = recurrence.Start;
            }

            var day = GetCurrentDayInPattern(from);

            return from.AddDays(GetNextDayInPattern(day, true));
        }

        public DateTime? GetNext(DateTime current) {
            if (!DaysOfWeek.Any()) {
                return null;
            }

            var day = GetCurrentDayInPattern(current);

            return current.AddDays(GetNextDayInPattern(day, false));
        }

        internal int GetCurrentDayInPattern(DateTime current) {
            var firstDayOfWeek = (int)GetFirstDayOfWeek();
            var totalDays = (current - recurrence.Start).Days;
            var weekDayCorrection = (int)recurrence.Start.DayOfWeek - firstDayOfWeek;
            
            if (weekDayCorrection < 0) {
                weekDayCorrection += 7;
            }
            
            return (totalDays + weekDayCorrection) % (7 * recurrence.Interval);
        }

        internal int GetNextDayInPattern(int day, bool allowCurrent) {
            var firstDayOfWeek = (int)GetFirstDayOfWeek();
            var daysInRange = DaysOfWeek.Select(d => ((int)d - firstDayOfWeek + 7) % 7).ToList();

            if (allowCurrent) {
                return daysInRange.Where(dayOfWeek => dayOfWeek >= day).DefaultIfEmpty(daysInRange.Min() + 7 * recurrence.Interval).Min() - day;
            }
            else {
                return daysInRange.Where(dayOfWeek => dayOfWeek > day).DefaultIfEmpty(daysInRange.Min() + 7 * recurrence.Interval).Min() - day;
            }
        }

        private DayOfWeek GetFirstDayOfWeek()
            => PeriodHandling switch {
                RecurrencePatternPeriodHandling.Calendar => FirstDayOfWeek,
                RecurrencePatternPeriodHandling.Ongoing => recurrence.Start.DayOfWeek,
                _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'")
            };
    }
}