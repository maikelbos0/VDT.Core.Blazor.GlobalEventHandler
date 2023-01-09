# VDT.Core.RecurringDates

Easily calculate ranges of recurring dates based on patterns in an intuitive and flexible way.

## Features

- Full range of patterns for calculating valid dates:
  - Daily, with weekend handling options
  - Weekly, with options for first day of the week and which days of the week are valid
  - Montly, either for days of the month or specific days of the week in a month
- Easy to use builder syntax
- Fully customizable and extensible: it's easy to add your own patterns

## Builder syntax

Builders are provided to help you easily set up a recurrence with patterns to calculate dates. Use the static `Recurs` class as a starting point to create a
`RecurrenceBuilder` that lets you fluently build your recurrence:

- `From(date)` sets a start date
- `Until(date)` sets an end date
- `StopAfter(date)` sets a maximum number of occurrences
- `Daily()` adds a pattern that repeats every day; it returns a builder that allows you to configure the day-based pattern
- `Every(interval).Days()` adds a pattern that repeats every `interval` days; it returns a builder that allows you to configure the day-based pattern
- `Weekly()` adds a pattern that repeats every week; it returns a builder that allows you to configure the week-based pattern
- `Every(interval).Weeks()` adds a pattern that repeats every `interval` weeks; it returns a builder that allows you to configure the week-based pattern
- `Monthly()` adds a pattern that repeats every month; it returns a builder that allows you to configure the month-based pattern
- `Every(interval).Months()` adds a pattern that repeats every `interval` months; it returns a builder that allows you to configure the month-based pattern

The pattern builders for days, weeks and months in turn allow you to configure them as desired:

- Daily patterns can be provided with:
  - A reference date to determine what day the pattern starts in case if an interval greater than 1
  - The option to include weekends, skip weekends or move any date that falls in a weekend to the following Monday or preceding Friday
- Weekly patterns can be provided with:
  - A reference date and first day of the week to determine what week the pattern starts in case if an interval greater than 1
  - The days of the week that are valid
  - If no days of the week are provided the day of the week of the reference date will be used
- Monthly patterns can be provided with:
  - A reference date to determine what month the pattern starts in case if an interval greater than 1
  - The days of the month that are valid
  - The ordinal days of the week in the month that are valid (e.g. last Friday of the month)
  - If no days of the month or week are provided, the day of the month of the reference date will be used

It's simple to chain calls to the above methods to set the limits and add multiple patterns for a single recurrence.

### Examples

```
// Build a recurrence that repeats every 9 days from January 1st 2022
var every9days = Recurs
    .Every(9).Days()
    .From(new DateTime(2022, 1, 1))
    .Build();

// Get all valid days for February 2022: 2022-02-06, 2022-02-15 and 2022-02-24
var dates = every9days.GetDates(new DateTime(2022, 2, 1), new DateTime(2022, 2, 28));


// Build a recurrence for the year 2022 with 2 patterns:
// - Every 2 weeks on Monday and Tuesday, determining the ongoing week using Sunday first day of the week
// - Every 1st and 3rd day of the month
var doublePattern = Recurs
    .From(new DateTime(2022, 1, 1))
    .Until(new DateTime(2022, 12, 31))
    .Every(2).Weeks().On(DayOfWeek.Monday, DayOfWeek.Tuesday).UsingFirstDayOfWeek(DayOfWeek.Sunday)
    .Monthly().On(1, 3)
    .Build();

// Get all valid days for February 2022: 2022-02-01, 2022-02-03, 2022-02-07, 2022-02-08, 2022-02-22 and 2022-02-23
var dates = doublePattern.GetDates(new DateTime(2022, 2, 1), new DateTime(2022, 2, 28));


// Build a recurrence that repeats every last Friday of the month for 3 occurrences, starting in April 2022
var fridays = Recurs
    .Monthly().On(DayOfWeekInMonth.Last, DayOfWeek.Friday)
    .From(new DateTime(2022, 4, 1))
    .StopAfter(3)
    .Build();

// Get all valid dates: 2022-04-29, 2022-05-27 and 2022-06-24
var dates = fridays.GetDates();
```

## Manual setup

Though the builder syntax makes it easy to write understandable recurrences, it is also possible to write recurrences and patterns by constructing the 
recurrence objects directly.

### Example

```
// Create a recurrence that repeats every 9 days from January 1st 2022
var every9days = new Recurrence(
    startDate: new DateTime(2022, 1, 1),
    endDate: DateTime.MaxValue,
    occurrences: null,
    patterns: new DailyRecurrencePattern(interval: 9, referenceDate: new DateTime(2022, 1, 1), weekendHandling: null)
);

// Get all valid days for February 2022: 2022-02-06, 2022-02-15 and 2022-02-24
var dates = every9days.GetDates(new DateTime(2022, 2, 1), new DateTime(2022, 2, 28));


// Create a recurrence for the year 2022 with 2 patterns:
// - Every 2 weeks on Monday and Tuesday, determining the ongoing week using Sunday first day of the week
// - Every 1st and 3rd day of the month
var doublePattern = new Recurrence(
    startDate: new DateTime(2022, 1, 1),
    endDate: new DateTime(2022, 12, 31),
    occurrences: null,
    patterns: new RecurrencePattern[] {
        new WeeklyRecurrencePattern(interval: 2, referenceDate: new DateTime(2022, 1, 1), firstDayOfWeek: DayOfWeek.Sunday, daysOfWeek: new[] { DayOfWeek.Monday, DayOfWeek.Tuesday }),
        new MonthlyRecurrencePattern(interval: 1, referenceDate: new DateTime(2022, 1, 1), daysOfMonth: new[] { 1, 3 })
    }
);

// Get all valid days for February 2022: 2022-02-01, 2022-02-03, 2022-02-07, 2022-02-08, 2022-02-22 and 2022-02-23
var dates = doublePattern.GetDates(new DateTime(2022, 2, 1), new DateTime(2022, 2, 28));


// Create a recurrence that repeats every last Friday of the month for 3 occurrences, starting in April 2022
var fridays = new Recurrence(
    startDate: new DateTime(2022, 4, 1),
    endDate: DateTime.MaxValue,
    occurrences: 3,
    patterns: new MonthlyRecurrencePattern(interval: 1, referenceDate: new DateTime(2022, 4, 1), daysOfWeek: new[] { (DayOfWeekInMonth.Last, DayOfWeek.Friday) })
);

// Get all valid dates: 2022-04-29, 2022-05-27 and 2022-06-24
var dates = fridays.GetDates();
```

## Custom patterns

If you have a need for adding custom patterns, you can create your own by inheriting from the base `RecurrencePattern` class. In addition, you can create your
own builder by inheriting from the base `RecurrencePatternBuilder<TBuilder>` and adding extension methods to the `RecurrenceBuilder` and 
`RecurrencePatternBuilderStart` classes.

Unfortunately compiler restrictions do not allow you to extend the static `Recurs` class, so you'll need to either manually instantiate the `RecurrenceBuilder`
class, start with one of the existing starting methods on the `Recurs` class, or create your own starting point.

### Example

```
public class AnnualRecurrencePattern : RecurrencePattern {
    private readonly HashSet<int> daysOfYear = new();

    public AnnualRecurrencePattern(int interval, DateTime referenceDate, IEnumerable<int> daysOfYear) : base(interval, referenceDate) {
        this.daysOfYear.UnionWith(daysOfYear);
    }

    public override bool IsValid(DateTime date)
        => daysOfYear.Contains(date.DayOfYear) && (Interval == 1 || (date.Year - ReferenceDate.Year) % Interval == 0);
}

public class AnnualRecurrencePatternBuilder : RecurrencePatternBuilder<AnnualRecurrencePatternBuilder> {
    public HashSet<int> DaysOfYear { get; set; } = new HashSet<int>();

    public AnnualRecurrencePatternBuilder(RecurrenceBuilder recurrenceBuilder, int interval) : base(recurrenceBuilder, interval) { }

    public AnnualRecurrencePatternBuilder On(params int[] days)
        => On(days.AsEnumerable());

    public AnnualRecurrencePatternBuilder On(IEnumerable<int> days) {
        DaysOfYear.UnionWith(days);
        return this;
    }

    public override RecurrencePattern BuildPattern()
        => new AnnualRecurrencePattern(Interval, ReferenceDate ?? RecurrenceBuilder.StartDate, DaysOfYear);
}

public static class RecurrenceBuilderExtensions {
    public static AnnualRecurrencePatternBuilder Anually(this IRecurrenceBuilder recurrenceBuilder) {
        var builder = new AnnualRecurrencePatternBuilder(recurrenceBuilder.GetRecurrenceBuilder(), 1);
        recurrenceBuilder.GetRecurrenceBuilder().PatternBuilders.Add(builder);
        return builder;
    }
}

public static class RecurrencePatternBuilderStartExtensions {
    public static AnnualRecurrencePatternBuilder Years(this RecurrencePatternBuilderStart start) {
        var builder = new AnnualRecurrencePatternBuilder(start.RecurrenceBuilder, start.Interval);
        start.RecurrenceBuilder.PatternBuilders.Add(builder);
        return builder;
    }

}
```

```
// Create a recurrence that repeats every 2 years on day 100 and every year on day 300, starting in 2010
var recurrence = Recurs
    .Every(2).Years().On(100)
    .Anually().On(300)
    .From(new DateTime(2010, 1, 1))
    .Build();

// Get all valid days for the years 2010 to 2012; 2010-04-10, 2010-10-27, 2011-10-27, 2012-04-09 and 2012-10-26
var dates = recurrence.GetDates(new DateTime(2010, 1, 1), new DateTime(2012, 12, 31)).ToList();

```