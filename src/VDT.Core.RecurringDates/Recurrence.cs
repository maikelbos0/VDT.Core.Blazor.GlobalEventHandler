using System;
using System.Collections.Generic;

namespace VDT.Core.RecurringDates {
    public class Recurrence {
        public DateTime Start { get; }

        public DateTime End { get; }

        // TODO allow multiple patterns
        public RecurrencePattern Pattern { get; }

        public Recurrence(DateTime start, DateTime end, RecurrencePattern pattern) {
            Start = start;
            End = end;
            Pattern = pattern;
        }

        public IEnumerable<DateTime> GetDates(DateTime? from = null, DateTime? to = null) {
            var current = (from.HasValue && from.Value > Start ? from.Value : Start).Date;
            var end = (to.HasValue && to.Value < End ? to.Value : End).Date;

            while (current <= end) {
                if (Pattern.IsValid(current)) {
                    yield return current;
                }

                current = current.AddDays(1);
            }
        }

        public IEnumerable<DateTime> GetDates(int count, DateTime? from = null) {
            var current = (from.HasValue && from.Value > Start ? from.Value : Start).Date;

            while (current <= End && count-- > 0) {
                if (Pattern.IsValid(current)) {
                    yield return current;
                }

                current = current.AddDays(1);
            }
        }
    }
}