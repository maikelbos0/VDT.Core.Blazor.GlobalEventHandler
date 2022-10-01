namespace VDT.Core.RecurringDates {
    public class DailyRecurrenceOptions : IRecurrenceOptions {
        public bool IncludingWeekends { get; set; } = true;
    }
}