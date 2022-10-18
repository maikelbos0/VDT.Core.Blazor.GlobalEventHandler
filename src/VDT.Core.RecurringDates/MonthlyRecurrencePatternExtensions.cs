namespace VDT.Core.RecurringDates {
    public static class MonthlyRecurrencePatternExtensions {
        public static MonthlyRecurrencePattern UseCalendarMonths(this MonthlyRecurrencePattern pattern) {
            pattern.PeriodHandling = RecurrencePatternPeriodHandling.Calendar;
            return pattern;
        }

        public static MonthlyRecurrencePattern UseOngoingMonths(this MonthlyRecurrencePattern pattern) {
            pattern.PeriodHandling = RecurrencePatternPeriodHandling.Ongoing;
            return pattern;
        }
    }
}