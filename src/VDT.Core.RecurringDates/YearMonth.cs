namespace VDT.Core.RecurringDates {
    internal class YearMonth {
        public YearMonth(uint year, uint month) {
            Value = year * 12 + month;
        }

        internal uint Value { get; set; }
        internal uint Year {
            get => Value / 12;
            set => Value = Value % 12 + value * 12;
        }
        internal uint Month {
            get => Value % 12;
            set => Value += value - Value % 12;
        }
    }
}