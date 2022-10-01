namespace VDT.Core.RecurringDates {
    public class Recurrence {
        private int interval = 1;

        public int Interval {
            get {
                return interval;
            }
            set {
                if (interval < 1) {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Expected {nameof(value)} to be at least 1 but found {value}.");
                }

                interval = value;
            }
        }

        public IRecurrenceOptions Options { get; set; } = new NoRecurrenceOptions();

        public DateTime StartDate { get; set; } = DateTime.MinValue;

        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        // TODO?
        //public int? NumberOfRepetitions { get; set; }

        public IEnumerable<DateTime> GetRange() {
            throw new NotImplementedException();
        }
    }
}