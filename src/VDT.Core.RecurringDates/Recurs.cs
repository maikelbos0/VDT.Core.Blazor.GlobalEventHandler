using System;

namespace VDT.Core.RecurringDates {
    public static class Recurs {
        public static IRecurrenceBuilder From(DateTime startDate) => new RecurrenceBuilder().From(startDate);

        public static IRecurrenceBuilder Until(DateTime endDate) => new RecurrenceBuilder().Until(endDate);

        public static IRecurrenceBuilder StopAfter(int occurrences) => new RecurrenceBuilder().StopAfter(occurrences);

        public static DailyRecurrencePatternBuilder Daily() => new RecurrenceBuilder().Daily();

        public static WeeklyRecurrencePatternBuilder Weekly() => new RecurrenceBuilder().Weekly();

        public static MonthlyRecurrencePatternBuilder Monthly() => new RecurrenceBuilder().Monthly();

        public static RecurrencePatternBuilderStart Every(int interval) => new RecurrenceBuilder().Every(interval);
    }
}