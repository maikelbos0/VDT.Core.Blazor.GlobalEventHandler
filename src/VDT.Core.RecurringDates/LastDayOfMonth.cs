namespace VDT.Core.RecurringDates {
    /// <summary>
    /// Indicates a specific day at the end of the month
    /// </summary>
    public enum LastDayOfMonth {
        /// <summary>
        /// Last day of the month
        /// </summary>
        Last = 0,

        /// <summary>
        /// Second last or penultimate day of the month
        /// </summary>
        SecondLast = 1,

        /// <summary>
        /// Third last day of the month
        /// </summary>
        ThirdLast = 2,

        /// <summary>
        /// Fourth last day of the month
        /// </summary>
        FourthLast = 3,

        /// <summary>
        /// Fifth last day of the month
        /// </summary>
        FifthLast = 4
    }
}
