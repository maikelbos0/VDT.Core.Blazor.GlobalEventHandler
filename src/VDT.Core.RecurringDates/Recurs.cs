using System;

namespace VDT.Core.RecurringDates {
    public static class Recurs {
        public static RecurrenceBuilder From(DateTime startDate) => new RecurrenceBuilder().From(startDate);

        public static RecurrenceBuilder Until(DateTime endDate) => new RecurrenceBuilder().Until(endDate);

        public static RecurrenceBuilder StopAfter(int occurrences) => new RecurrenceBuilder().StopAfter(occurrences);

        public static DailyRecurrencePatternBuilder Daily() => new RecurrenceBuilder().Daily();

        public static WeeklyRecurrencePatternBuilder Weekly() => new RecurrenceBuilder().Weekly();

        public static MonthlyRecurrencePatternBuilder Monthly() => new RecurrenceBuilder().Monthly();

        public static RecurrencePatternBuilderStart Every(int interval) => new RecurrenceBuilder().Every(interval);
    }
}