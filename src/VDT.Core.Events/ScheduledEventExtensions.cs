using NCrontab;
using System;

namespace VDT.Core.Events {
    internal static class ScheduledEventExtensions {
        internal static TimeSpan GetTimeToNextDispatch(this IScheduledEvent scheduledEvent, DateTime utcNow) {
            var previousDispatch = (scheduledEvent.PreviousDispatch ?? utcNow).ToUniversalTime();
            var crontabSchedule = CrontabSchedule.Parse(scheduledEvent.CronExpression, new CrontabSchedule.ParseOptions {
                IncludingSeconds = scheduledEvent.CronExpressionIncludingSeconds
            });

            var time = crontabSchedule.GetNextOccurrence(previousDispatch).ToUniversalTime() - utcNow;

            if (time > TimeSpan.Zero) {
                return time;
            }
            else {
                return TimeSpan.Zero;
            }
        }
    }
}
