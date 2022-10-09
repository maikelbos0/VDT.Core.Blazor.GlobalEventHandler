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

        DateTime? IRecurrencePattern.GetFirst(int interval, DateTime start, DateTime from) {
            if (!Days.Any()) {
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
            var candidateDays = Days.Select(day => (day + 7 - firstDayOfWeek) % 7);
            
            if (!candidateDays.Any(day => day + firstBaseDays >= minimumDays)) {
                firstBaseDays += interval * 7;
            }

            return DateTime.MinValue.AddDays(firstBaseDays + candidateDays.Where(day => day + firstBaseDays >= minimumDays).Min());
        }

        DateTime? IRecurrencePattern.GetNext(int interval, DateTime current) {
            throw new NotImplementedException();
        }

        private DayOfWeek GetFirstDayOfWeek(DateTime start) {
            return PeriodHandling switch {
                RecurrencePatternPeriodHandling.Calendar => FirstDayOfWeek,
                RecurrencePatternPeriodHandling.Ongoing => start.DayOfWeek,
                _ => throw new NotImplementedException($"No implementation found for {nameof(RecurrencePatternPeriodHandling)} '{PeriodHandling}'")
            };
        }

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