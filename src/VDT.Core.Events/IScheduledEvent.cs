namespace VDT.Core.Events {
    /// <summary>
    /// Specifies the properties of an event type that should be dispatched at times defined in a schedule
    /// </summary>
    public interface IScheduledEvent {
        /// <summary>
        /// Cron expression defining the schedule on which the event should be dispatched
        /// </summary>
        string CronExpression { get; }
    }
}
