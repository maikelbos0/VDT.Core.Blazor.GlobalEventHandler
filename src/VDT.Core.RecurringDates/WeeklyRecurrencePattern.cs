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

        DateTime? IRecurrencePattern.GetFirst(DateTime from) {
            if (!DaysOfWeek.Any()) {
                return null;
            }

            var firstDayOfWeek = GetFirstDayOfWeek();
            var startDaysCorrected = (recurrence.Start - DateTime.MinValue).Days + (firstDayOfWeek - recurrence.Start.DayOfWeek - 7) % -7;
            var minimum = from;

            if (recurrence.Start > from) {
                minimum = recurrence.Start;
            }

            var minimumDays = (minimum - DateTime.MinValue).Days;
            var minimumDaysCorrected = minimumDays + (firstDayOfWeek - minimum.DayOfWeek - 7) % -7;
            var iterations = (minimumDaysCorrected - startDaysCorrected - 1) / (recurrence.Interval * 7);
            var firstBaseDays = startDaysCorrected + iterations * recurrence.Interval * 7;            
            var candidateDaysOfWeek = DaysOfWeek.Select(day => (day + 7 - firstDayOfWeek) % 7);
            
            if (!candidateDaysOfWeek.Any(dayOfWeek => dayOfWeek + firstBaseDays >= minimumDays)) {
                firstBaseDays += recurrence.Interval * 7;
            }

            return DateTime.MinValue.AddDays(firstBaseDays + candidateDaysOfWeek.Where(dayOfWeek => dayOfWeek + firstBaseDays >= minimumDays).Min());
        }

        DateTime? IRecurrencePattern.GetNext(DateTime current) {
            if (!DaysOfWeek.Any()) {
                return null;
            }

            var dayOfWeekMap = GetDayOfWeekMap();

            return current.AddDays(dayOfWeekMap[current.DayOfWeek]);
        }

        internal Dictionary<DayOfWeek, int> GetDayOfWeekMap() {
            var daysOfWeek = DaysOfWeek.ToList();
            var dayOfWeekMap = new Dictionary<DayOfWeek, int>();

            for (var i = 0; i < daysOfWeek.Count - 1; i++) {
                dayOfWeekMap[daysOfWeek[i]] = daysOfWeek[i + 1] - daysOfWeek[i];
            }

            dayOfWeekMap[daysOfWeek[daysOfWeek.Count - 1]] = 7 + daysOfWeek[0] - daysOfWeek[daysOfWeek.Count - 1];

            if (recurrence.Interval > 1) {
                dayOfWeekMap[GetLastApplicableDayOfWeek()] += (recurrence.Interval - 1) * 7;
            }

            return dayOfWeekMap;
        }

        internal DayOfWeek GetLastApplicableDayOfWeek() {
            var firstDayOfWeek = GetFirstDayOfWeek();

            if (DaysOfWeek.Contains(firstDayOfWeek)) {
                return firstDayOfWeek;
            }

            return DaysOfWeek.Cast<DayOfWeek?>().LastOrDefault(dayOfWeek => dayOfWeek!.Value < firstDayOfWeek) ?? DaysOfWeek.Last();
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