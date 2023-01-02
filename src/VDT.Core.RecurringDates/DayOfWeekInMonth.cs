namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Indicates the ordinal occurrence inside the month of given the day of week that should be used
    /// </summary>
    public enum DayOfWeekInMonth {
        /// <summary>
        /// First occurrence of the given day of week
        /// </summary>
        First = 0,

        /// <summary>
        /// Second occurrence of the given day of week
        /// </summary>
        Second = 1,

        /// <summary>
        /// Third occurrence of the given day of week
        /// </summary>
        Third = 2,

        /// <summary>
        /// Fourth occurrence of the given day of week
        /// </summary>
        Fourth = 3,

        /// <summary>
        /// Last occurrence of the given day of week; this is either the fourth or fifth depending on the month
        /// </summary>
        Last = 4
    }
}