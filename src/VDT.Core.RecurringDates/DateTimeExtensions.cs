using System;

namespace VDT.Core.RecurringDates {
    internal static class DateTimeExtensions {
        internal static int TotalMonths(this DateTime date) => date.Year * 12 + date.Month;

        internal static int DaysInMonth(this DateTime date) => DateTime.DaysInMonth(date.Year, date.Month);
    }
}
