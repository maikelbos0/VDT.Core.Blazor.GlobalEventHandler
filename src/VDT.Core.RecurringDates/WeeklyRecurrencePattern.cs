using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace VDT.Core.RecurringDates {
    public class WeeklyRecurrencePattern : IRecurrencePattern {
        public RecurrencePatternPeriodHandling PeriodHandling { get; set; } = RecurrencePatternPeriodHandling.Ongoing;
        public DayOfWeek FirstDayOfWeek { get; set; } = Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
        public SortedSet<DayOfWeek> DaysOfWeek { get; set; } = new SortedSet<DayOfWeek>();
        // TODO add using start day of week as day?

        DateTime? IRecurrencePattern.GetFirst(int interval, DateTime start, DateTime from) {
            if (!DaysOfWeek.Any()) {
                return null;
            }

            var firstDayOfWeek = GetFirstDayOfWeek(start);
            var startDaysCorrected = (start - DateTime.MinValue).Days + (firstDayOfWeek - start.DayOfWeek - 7) % -7;
            var minimum = from;

            if (start > from) {
                minimum = start;
            }

            var minimumDays = (minimum - DateTime.MinValue).Days;
            var minimumDaysCorrected = minimumDays + (firstDayOfWeek - minimum.DayOfWeek - 7) % -7;
            var iterations = (minimumDaysCorrected - startDaysCorrected - 1) / (interval * 7);
            var firstBaseDays = startDaysCorrected + iterations * interval * 7;            
            var candidateDaysOfWeek = DaysOfWeek.Select(day => (day + 7 - firstDayOfWeek) % 7);
            
            if (!candidateDaysOfWeek.Any(dayOfWeek => dayOfWeek + firstBaseDays >= minimumDays)) {
                firstBaseDays += interval * 7;
            }

            return DateTime.MinValue.AddDays(firstBaseDays + candidateDaysOfWeek.Where(dayOfWeek => dayOfWeek + firstBaseDays >= minimumDays).Min());
        }

        DateTime? IRecurrencePattern.GetNext(int interval, DateTime start, DateTime current) {
            if (!DaysOfWeek.Any()) {
                return null;
            }

            var dayOfWeekMap = GetDayOfWeekMap(interval, start);

            return current.AddDays(dayOfWeekMap[current.DayOfWeek]);
        }

        internal Dictionary<DayOfWeek, int> GetDayOfWeekMap(int interval, DateTime start) {
            var daysOfWeek = DaysOfWeek.ToList();
            var dayOfWeekMap = new Dictionary<DayOfWeek, int>();

            for (var i = 0; i < daysOfWeek.Count - 1; i++) {
                dayOfWeekMap[daysOfWeek[i]] = daysOfWeek[i + 1] - daysOfWeek[i];
            }

            dayOfWeekMap[daysOfWeek[daysOfWeek.Count - 1]] = 7 + daysOfWeek[0] - daysOfWeek[daysOfWeek.Count - 1];

            if (interval > 1) {
                dayOfWeekMap[GetLastApplicableDayOfWeek(start)] += (interval - 1) * 7;
            }

            return dayOfWeekMap;
        }

        internal DayOfWeek GetLastApplicableDayOfWeek(DateTime start) {
            var firstDayOfWeek = GetFirstDayOfWeek(start);

            if (DaysOfWeek.Contains(firstDayOfWeek)) {
                return firstDayOfWeek;
            }

            return DaysOfWeek.Cast<DayOfWeek?>().LastOrDefault(dayOfWeek => dayOfWeek!.Value < firstDayOfWeek) ?? DaysOfWeek.Last();
        }

        private DayOfWeek GetFirstDayOfWeek(DateTime start) {
            return PeriodHandling switch {
                RecurrencePatternPeriodHandling.Calendar => FirstDayOfWeek,
                RecurrencePatternPeriodHandling.Ongoing => start.DayOfWeek,
                _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'")
            };
        }
    }
}