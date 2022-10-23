using System;

namespace VDT.Core.RecurringDates {
    internal static class DateTimeExtensions {
        internal static int DaysInMonth(this DateTime date) => DateTime.DaysInMonth(date.Year, date.Month);
    }
}
