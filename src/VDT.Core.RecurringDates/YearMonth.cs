namespace VDT.Core.RecurringDates {
    internal class YearMonth {
        private uint totalMonths;

        public YearMonth(uint year, uint month) {
            totalMonths = year * 12 + month;
        }

        internal uint Year {
            get => totalMonths / 12;
            set => totalMonths = totalMonths % 12 + value * 12;
        }
        internal uint Month {
            get => totalMonths % 12;
            set => totalMonths += value - totalMonths % 12;
        }

        public void Deconstruct(out uint year, out uint month) {
            year = Year;
            month = Month;
        }
    }
}