using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class Recurrence {
        private readonly List<RecurrencePattern> patterns = new();

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public int? Occurrences { get; }

        public IReadOnlyList<RecurrencePattern> Patterns => new ReadOnlyCollection<RecurrencePattern>(patterns);

        public Recurrence(DateTime startDate, DateTime endDate, int? occurrences, params RecurrencePattern[] patterns) : this(startDate, endDate, occurrences, patterns.AsEnumerable()) { }

        public Recurrence(DateTime startDate, DateTime endDate, int? occurrences, IEnumerable<RecurrencePattern> patterns) {
            StartDate = startDate.Date;
            EndDate = endDate.Date;
            Occurrences = occurrences;
            this.patterns.AddRange(patterns);
        }

        public IEnumerable<DateTime> GetDates(DateTime? from = null, DateTime? to = null) {
            if (from == null || from < StartDate) {
                from = StartDate;
            }

            if (to == null || to > EndDate) {
                to = EndDate;
            }

            if (Occurrences == null) {
                return GetDatesWithoutOccurrences(from.Value.Date, to.Value.Date);
            }
            else {
                return GetDatesWithOccurrences(from.Value.Date, to.Value.Date);
            }
            
            IEnumerable<DateTime> GetDatesWithoutOccurrences(DateTime from, DateTime to) {
                var currentDate = from;

                while (currentDate <= to) {
                    if (patterns.Any(pattern => pattern.IsValid(currentDate))) {
                        yield return currentDate;
                    }

                    currentDate = currentDate.AddDays(1);
                }
            }

            IEnumerable<DateTime> GetDatesWithOccurrences(DateTime from, DateTime to) {
                var occurrences = 0;
                var currentDate = StartDate;

                while (currentDate <= to && Occurrences > occurrences) {
                    if (patterns.Any(pattern => pattern.IsValid(currentDate))) {
                        occurrences++;

                        if (currentDate >= from) {
                            yield return currentDate;
                        }
                    }

                    currentDate = currentDate.AddDays(1);
                }
            }
        }
    }
}