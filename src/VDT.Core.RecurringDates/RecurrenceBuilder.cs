﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.RecurringDates {
    public class RecurrenceBuilder {
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        public List<RecurrencePatternBuilder> PatternBuilders { get; set; } = new();

        public RecurrenceBuilder StartsOn(DateTime startDate) {
            StartDate = startDate.Date;
            return this;
        }

        public RecurrenceBuilder EndsOn(DateTime endDate) {
            EndDate = endDate.Date;
            return this;
        }

        public Recurrence Build() {
            return new Recurrence(StartDate, EndDate, PatternBuilders.Select(builder => builder.Build()));
        }
    }
}