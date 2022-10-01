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

        public IRecurrenceOptions Options { get; set; } = new NoRecurrenceOptions();

        public DateTime Start { get; set; } = DateTime.MinValue;

        public DateTime End { get; set; } = DateTime.MaxValue;

        // TODO?
        //public int? NumberOfRepetitions { get; set; }

        public IEnumerable<DateTime> GetDates(DateTime? from = null, DateTime? to = null) {
            throw new NotImplementedException();
        }
    }
}