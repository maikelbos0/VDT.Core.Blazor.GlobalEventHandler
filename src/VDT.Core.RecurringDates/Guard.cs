using System;

namespace VDT.Core.RecurringDates {
    internal static class Guard {
        internal static int IsPositive(int value) {
            if (value < 1) {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Expected {nameof(value)} to be at least 1 but found {value}.");
            }

            return value;
        }
    }
}