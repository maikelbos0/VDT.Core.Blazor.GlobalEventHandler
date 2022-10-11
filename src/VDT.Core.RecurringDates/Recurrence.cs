using System;
using System.Collections.Generic;

namespace VDT.Core.RecurringDates {
    public class Recurrence {
        private int interval = 1;

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

        public IRecurrencePattern Pattern { get; set; } = new NoRecurrencePattern();

        public IEnumerable<DateTime> GetDates(DateTime? from = null, DateTime? to = null) {
            var current = Pattern.GetFirst(from ?? Start);

            while (current != null && current <= End && (to == null || current <= to)) {
                yield return current.Value;

                current = Pattern.GetNext(current.Value);
            }
        }

        public IEnumerable<DateTime> GetDates(int count, DateTime? from = null) {
            var current = Pattern.GetFirst(from ?? Start);

            while (current != null && current <= End && count-- > 0) {
                yield return current.Value;

                current = Pattern.GetNext(current.Value);
            }
        }
    }
}