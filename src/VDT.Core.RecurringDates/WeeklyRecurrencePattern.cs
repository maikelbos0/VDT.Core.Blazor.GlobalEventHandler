using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePattern : IRecurrencePattern {
        public RecurrencePatternPeriodHandling PeriodHandling { get; set; } = RecurrencePatternPeriodHandling.Ongoing;
        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        public HashSet<DayOfWeek> Days { get; set; } = new HashSet<DayOfWeek>();
        // TODO add using start day of week as day?

        // TODO clean up
        DateTime? IRecurrencePattern.GetFirst(int interval, DateTime start, DateTime from) {
            if (!Days.Any()) {
                return null;
            }

            var firstDayOfWeek = PeriodHandling switch {
                RecurrencePatternPeriodHandling.Calendar => FirstDayOfWeek,
                RecurrencePatternPeriodHandling.Ongoing => start.DayOfWeek,
                _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'")
            };

            var startDays = (start - DateTime.MinValue).Days;
            var startDaysCorrected = startDays + GetWeekStartCorrection(start);
            var first = from;

            if (start > from) {
                first = start;
            }

            var firstDays = (first - DateTime.MinValue).Days;
            var firstDaysCorrected = firstDays + GetWeekStartCorrection(first);

            var iterations = (firstDaysCorrected - startDaysCorrected - 1) / (interval * 7);

            var days = startDaysCorrected + iterations * interval * 7;

            var candidates = Days.Select(d => (d + 7 - firstDayOfWeek) % 7);

            if (!candidates.Any(c => c + days >= firstDays)) {
                days += interval * 7;
            }

            return DateTime.MinValue.AddDays(days + candidates.Where(c => c + days >= firstDays).Min(c => c));
        }

        DateTime? IRecurrencePattern.GetNext(int interval, DateTime current) {
            throw new NotImplementedException();
        }

        internal int GetWeekStartCorrection(DateTime date)
            => PeriodHandling switch {
                RecurrencePatternPeriodHandling.Calendar => (FirstDayOfWeek - date.DayOfWeek - 7) % -7,
                RecurrencePatternPeriodHandling.Ongoing => 0,
                _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'")
            };

        internal Dictionary<DayOfWeek, int> GetDayMap() {
            var days = Days.OrderBy(day => day).ToList();
            var dayMap = new Dictionary<DayOfWeek, int>();

            for (var i = 0; i < days.Count - 1; i++) {
                dayMap[days[i]] = days[i + 1] - days[i];
            }

            dayMap[days[days.Count - 1]] = 7 + days[0] - days[days.Count - 1];

            return dayMap;
        }
    }
}