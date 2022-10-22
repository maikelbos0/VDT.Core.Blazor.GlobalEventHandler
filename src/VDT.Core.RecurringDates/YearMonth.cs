using System;

namespace VDT.Core.RecurringDates {
    internal class YearMonth {
        private int totalMonths;

        public YearMonth(int year, int month) {
            totalMonths = year * 12 + month;
        }

        internal int Year {
            get => totalMonths / 12;
            set => totalMonths = totalMonths % 12 + value * 12;
        }
        internal int Month {
            get => totalMonths % 12;
            set => totalMonths += value - totalMonths % 12;
        }

        public void Deconstruct(out int year, out int month) {
            year = Year;
            month = Month;
        }

        public static implicit operator YearMonth(DateTime date) => new YearMonth(date.Year, date.Month);
    }
}