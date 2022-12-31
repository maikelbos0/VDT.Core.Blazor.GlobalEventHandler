using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class Recurrence {
        private readonly List<RecurrencePattern> patterns = new();

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public IReadOnlyList<RecurrencePattern> Patterns => new ReadOnlyCollection<RecurrencePattern>(patterns);

        public Recurrence(DateTime startDate, DateTime endDate, params RecurrencePattern[] patterns) : this(startDate, endDate, patterns.AsEnumerable()) { }

        public Recurrence(DateTime startDate, DateTime endDate, IEnumerable<RecurrencePattern> patterns) {
            StartDate = startDate;
            EndDate = endDate;
            this.patterns.AddRange(patterns);
        }

        public IEnumerable<DateTime> GetDates(DateTime? from = null, DateTime? to = null) {
            var current = (from.HasValue && from.Value > StartDate ? from.Value : StartDate).Date;
            var endDate = (to.HasValue && to.Value < EndDate ? to.Value : EndDate).Date;

            while (current <= endDate) {
                if (patterns.Any(pattern => pattern.IsValid(current))) {
                    yield return current;
                }

                current = current.AddDays(1);
            }
        }

        public IEnumerable<DateTime> GetDates(int count, DateTime? from = null) {
            var current = (from.HasValue && from.Value > StartDate ? from.Value : StartDate).Date;

            while (current <= EndDate && count > 0) {
                if (patterns.Any(pattern => pattern.IsValid(current))) {
                    count--;
                    yield return current;
                }

                current = current.AddDays(1);
            }
        }
    }
}