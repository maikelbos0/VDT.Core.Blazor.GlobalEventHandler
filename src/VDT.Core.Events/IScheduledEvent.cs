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
        /// 
        /// </summary>
        bool CronExpressionIncludingSeconds { get; }
        
        /// <summary>
        /// Date and time the event was last dispatched; if null the current date and time is assumed
        /// </summary>
        DateTime? PreviousDispatch { get; set; }
    }
}
