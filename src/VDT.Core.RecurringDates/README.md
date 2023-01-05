# VDT.Core.RecurringDates

Easily calculate ranges of recurring dates based on patterns in an intuitive and flexible way, for example:

- Every 3 days in a certain period
- Every Friday from a certain date
- Every third Tuesday of every second month
- Every Monday and Friday for 11 occurrences

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

- `From` sets a start date before which no dates are valid
- `Until` sets an end date after which no dates are valid
- `StopAfter` sets a maximum number of occurrences after which no dates are valid
- `Daily` adds a pattern that repeats every day; it returns a builder that allows you to configure the day-based pattern
- `Every(x).Days()` adds a pattern that repeats every `x` days; it returns a builder that allows you to configure the day-based pattern
- `Weekly` adds a pattern that repeats every week; it returns a builder that allows you to configure the week-based pattern
- `Every(x).Weeks()` adds a pattern that repeats every `x` weeks; it returns a builder that allows you to configure the week-based pattern
- `Monthly` adds a pattern that repeats every month; it returns a builder that allows you to configure the month-based pattern
- `Every(x).Months()` adds a pattern that repeats every `x` months; it returns a builder that allows you to configure the month-based pattern

The pattern builders for days, weeks and months in turn allow you to configure them as desired:
- TODO: add builder methods + defaults

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

TODO

### Example

```
TODO?
```