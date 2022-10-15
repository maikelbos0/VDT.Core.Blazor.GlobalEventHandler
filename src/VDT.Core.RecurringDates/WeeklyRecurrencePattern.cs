using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePattern : IRecurrencePattern {
        private readonly Recurrence recurrence;

        public RecurrencePatternPeriodHandling PeriodHandling { get; set; } = RecurrencePatternPeriodHandling.Ongoing;

        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        public SortedSet<DayOfWeek> DaysOfWeek { get; set; } = new SortedSet<DayOfWeek>();

        // TODO add using start day of week as day?

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

            var firstDayOfWeek = GetFirstDayOfWeek();
            var day = ((from - recurrence.Start).Days - (firstDayOfWeek - recurrence.Start.DayOfWeek - 7) % -7) % (7 * recurrence.Interval);

            return from.AddDays(GetNextDay(day, true));
        }

        public DateTime? GetNext(DateTime current) {
            if (!DaysOfWeek.Any()) {
                return null;
            }

            var firstDayOfWeek = GetFirstDayOfWeek();
            var day = ((current - recurrence.Start).Days - (firstDayOfWeek - recurrence.Start.DayOfWeek - 7) % -7) % (7 * recurrence.Interval);

            return current.AddDays(GetNextDay(day, false));
        }

        internal int GetNextDay(int day, bool allowCurrent) {
            var firstDayOfWeek = (int)GetFirstDayOfWeek();
            var daysInRange = DaysOfWeek.Select(d => ((int)d - firstDayOfWeek + 7) % 7).ToList();

            if (allowCurrent) {
                return daysInRange.Where(dayOfWeek => dayOfWeek >= day).DefaultIfEmpty(daysInRange.Min() + 7 * recurrence.Interval).Min() - day;
            }
            else {
                return daysInRange.Where(dayOfWeek => dayOfWeek > day).DefaultIfEmpty(daysInRange.Min() + 7 * recurrence.Interval).Min() - day;
            }
        }

        private DayOfWeek GetFirstDayOfWeek() {
            return PeriodHandling switch {
                RecurrencePatternPeriodHandling.Calendar => FirstDayOfWeek,
                RecurrencePatternPeriodHandling.Ongoing => recurrence.Start.DayOfWeek,
                _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'")
            };
        }
    }
}