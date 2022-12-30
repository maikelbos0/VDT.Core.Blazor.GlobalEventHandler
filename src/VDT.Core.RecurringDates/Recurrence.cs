using System;
using System.Collections.Generic;

namespace VDT.Core.RecurringDates {
    public class Recurrence {
        private int interval = 1;

        // TODO move interval to patterns to allow multiple patterns in a sensible way
        public int Interval {
            get {
                return interval;
            }
            set {
                if (value < 1) {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Expected {nameof(value)} to be at least 1 but found {value}.");
                }

                interval = value;
            }
        }

        public DateTime Start { get; set; } = DateTime.MinValue;

        public DateTime End { get; set; } = DateTime.MaxValue;

        // TODO allow multiple patterns
        public IRecurrencePattern Pattern { get; set; } = new NoRecurrencePattern();

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