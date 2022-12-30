using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class Recurrence {
        private readonly List<RecurrencePattern> patterns = new();

        public DateTime Start { get; }

        public DateTime End { get; }

        public IReadOnlyList<RecurrencePattern> Patterns => new ReadOnlyCollection<RecurrencePattern>(patterns);

        public Recurrence(DateTime start, DateTime end, params RecurrencePattern[] patterns) : this(start, end, patterns.AsEnumerable()) { }

        public Recurrence(DateTime start, DateTime end, IEnumerable<RecurrencePattern> patterns) {
            Start = start;
            End = end;
            this.patterns.AddRange(patterns);
        }

        public IEnumerable<DateTime> GetDates(DateTime? from = null, DateTime? to = null) {
            var current = (from.HasValue && from.Value > Start ? from.Value : Start).Date;
            var end = (to.HasValue && to.Value < End ? to.Value : End).Date;

            while (current <= end) {
                if (patterns.Any(pattern => pattern.IsValid(current))) {
                    yield return current;
                }

                current = current.AddDays(1);
            }
        }

        public IEnumerable<DateTime> GetDates(int count, DateTime? from = null) {
            var current = (from.HasValue && from.Value > Start ? from.Value : Start).Date;

            while (current <= End && count > 0) {
                if (patterns.Any(pattern => pattern.IsValid(current))) {
                    count--;
                    yield return current;
                }

                current = current.AddDays(1);
            }
        }
    }
}