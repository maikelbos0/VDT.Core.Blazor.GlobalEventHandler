using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    internal static class Guard {
        internal static int IsPositive(int value) {
            if (value < 1) {
                throw new ArgumentOutOfRangeException(nameof(value), value, $"Expected {nameof(value)} to be at least 1 but found {value}.");
            }

            return value;
        }

        internal static IEnumerable<int> ArePositive(IEnumerable<int> values) {
            if (values.Any(value => value < 1)) {
                throw new ArgumentOutOfRangeException(nameof(values), values, $"Expected all {nameof(values)} to be at least 1 but found {string.Join(",", values)}.");
            }

            return values;
        }
    }
}