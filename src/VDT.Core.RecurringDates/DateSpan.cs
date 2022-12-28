using System;

namespace VDT.Core.RecurringDates {
    public struct DateSpan : IComparable<DateSpan> {
        public static DateSpan operator +(DateSpan a, DateSpan b) => new DateSpan(a.Months + b.Months, a.Days + b.Days);
        public static DateSpan operator -(DateSpan a, DateSpan b) => new DateSpan(a.Months - b.Months, a.Days - b.Days);
        public static bool operator >(DateSpan a, DateSpan b) => a.CompareTo(b) > 0;
        public static bool operator <(DateSpan a, DateSpan b) => a.CompareTo(b) < 0;
        public static bool operator >=(DateSpan a, DateSpan b) => a.CompareTo(b) >= 0;
        public static bool operator <=(DateSpan a, DateSpan b) => a.CompareTo(b) <= 0;

        public int Months { get; }
        public int Days { get; }

        public DateSpan(int months, int days) {
            Months = months;
            Days = days;
        }

        public int CompareTo(DateSpan other) {
            if (Months == other.Months) {
                return Days.CompareTo(other.Days);
            }
            else {
                return Months.CompareTo(other.Months);
            }
        }
    }
}
