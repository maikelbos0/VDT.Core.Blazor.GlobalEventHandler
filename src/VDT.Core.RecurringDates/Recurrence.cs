using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// A recurrence to determine valid dates for the given patterns
    /// </summary>
    public class Recurrence {
        private readonly List<RecurrencePattern> patterns = new();

        /// <summary>
        /// Gets the inclusive start date for this recurrence
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the inclusive end date for this recurrence
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// Gets the maximum number of occurrences for this recurrence; if <see langword="null"/> it repeats without limit
        /// </summary>
        public int? Occurrences { get; }

        /// <summary>
        /// Gets the recurrence patterns that this recurrence will use to determine valid dates
        /// </summary>
        public IReadOnlyList<RecurrencePattern> Patterns => new ReadOnlyCollection<RecurrencePattern>(patterns);

        /// <summary>
        /// Create a recurrence to determine valid dates for the given patterns
        /// </summary>
        /// <param name="startDate">Inclusive start date for this recurrence</param>
        /// <param name="endDate">Inclusive end date for this recurrence</param>
        /// <param name="occurrences">Maximum number of occurrences for this recurrence</param>
        /// <param name="patterns">Recurrence patterns that this recurrence will use to determine valid dates</param>
        public Recurrence(DateTime startDate, DateTime endDate, int? occurrences, params RecurrencePattern[] patterns) : this(startDate, endDate, occurrences, patterns.AsEnumerable()) { }

        /// <summary>
        /// Create a recurrence to determine valid dates for the given patterns
        /// </summary>
        /// <param name="startDate">Inclusive start date for this recurrence</param>
        /// <param name="endDate">Inclusive end date for this recurrence</param>
        /// <param name="occurrences">Maximum number of occurrences for this recurrence</param>
        /// <param name="patterns">Recurrence patterns that this recurrence will use to determine valid dates</param>
        public Recurrence(DateTime startDate, DateTime endDate, int? occurrences, IEnumerable<RecurrencePattern> patterns) {
            StartDate = startDate.Date;
            EndDate = endDate.Date;
            Occurrences = occurrences;
            this.patterns.AddRange(patterns);
        }

        /// <summary>
        /// Gets valid dates for this recurrence within the specified optional range
        /// </summary>
        /// <param name="from">If provided, no dates before this date will be returned</param>
        /// <param name="to">If provided, no dates after this date will be returned</param>
        /// <returns>Valid dates for this recurrence within the specified optional range</returns>
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