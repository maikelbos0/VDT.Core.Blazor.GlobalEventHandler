using System;

namespace VDT.Core.Events {
    /// <summary>
    /// Specifies the properties of an event type that should be dispatched at times defined in a schedule
    /// </summary>
    public interface IScheduledEvent {
        /// <summary>
        /// Cron expression defining the schedule on which the event should be dispatched
        /// </summary>
        string CronExpression { get; }

        /// <summary>
        /// Indicates whether or not the cron expression that defines the schedule includes seconds or only has minute precision
        /// </summary>
        bool CronExpressionIncludesSeconds { get; }
        
        /// <summary>
        /// Date and time the event was last dispatched; if null the current date and time is assumed
        /// </summary>
        DateTime? PreviousDispatch { get; set; }
    }
}
