using System;

namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Base class for recurring date patterns
    /// </summary>
    public abstract class RecurrencePattern {
        /// <summary>
        /// Interval between occurrences of this pattern
        /// </summary>
        public int Interval { get; }

        /// <summary>
        /// Date to use as a reference for calculating periods when the interval is greater than 1
        /// </summary>
        public DateTime ReferenceDate { get; }

        /// <summary>
        /// Create a recurring date pattern
        /// </summary>
        /// <param name="interval">Interval between occurrences of this pattern</param>
        /// <param name="referenceDate">Date to use as a reference for calculating periods when the interval is greater than 1</param>
        public RecurrencePattern(int interval, DateTime referenceDate) {
            Interval = Guard.IsPositive(interval);
            ReferenceDate = referenceDate;
        }

        /// <summary>
        /// Determine whether a given date is valid according to this recurrence pattern
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <returns><see langword="true"/> if the given date is valid according to this recurrence pattern; otherwise <see langword="false"/></returns>
        public abstract bool IsValid(DateTime date);
    }
}