![.NET Core](https://github.com/StevenRasmussen/EFCore.SqlServer.NodaTime/workflows/.NET%20Core/badge.svg?branch=master)

# EFCore.SqlServer.NodaTime

Adds native support to EntityFrameworkCore for SQL Server for the [NodaTime](https://nodatime.org/) types.

The following types are supported:
* Instant
* OffsetDateTime
* LocalDateTime
* LocalDate
* LocalTime
* Duration

## Unit Tests
All types and their methods have unit tests written to verify that the SQL is translated as expected. See individual tests for more information.

## Usage
To use, simply install the NuGet package:
```shell
Install-Package SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime
```

Then call the `UseNodaTime()` method as part of your SqlServer configuration during the `UseSqlServer` method call:
```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

options.UseSqlServer("your DB Connection",
                    x => x.UseNodaTime());
```

## DATEADD Support
The SQL `DATEADD` function is supported for the following types:
* Instant (extension methods)
* OffsetDateTime (native and some extension methods)
* LocalDateTime (native and some extension methods)
* LocalDate (native and some extension methods)
* LocalTime (native and some extension methods)
* Duration (native and some extension methods)

**Note**: Please add a using statement in order to use the extension methods:
```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
```

### Supported Methods
* PlusYears
* PlusMonths
* PlusDays
* PlusHours
* PlusMinutes
* PlusSeconds
* PlusMilliseconds

```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// PlusYears
await this.Db.RaceResult
    .Where(r => r.StartTime.PlusYears(1) >= Instant.FromUtc(2019, 7, 1, 1, 0))
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] 
// FROM [RaceResult] AS [r] WHERE DATEADD(year, CAST(1 AS int), [r].[StartTime]) >= '2019-07-01T01:00:00.0000000Z'
```

## DATEPART Support
The SQL `DATEPART` function is supported for the following types:
* Instant (extension methods)
* OffsetDateTime (native and some extension methods)
* LocalDateTime (native and some extension methods)
* LocalDate (native and some extension methods)
* LocalTime (native and some extension methods)
* Duration (native and some extension methods)

**Note**: Please add a using statement in order to use the extension methods:
```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
```

### Supported Parts
* Year
* Quarter
* Month
* DayOfYear
* Day
* Week
* WeekDay
* Hour
* Minute
* Second
* Millisecond
* Microsecond
* Nanosecond
* TzOffset
* IsoWeek

```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// Compare the 'Year' DatePart
await this.Db.RaceResult
    .Where(r => r.StartTime.Year() == 2019)
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[EndTime], [r].[StartTime], [r].[StartTimeOffset] 
// FROM [RaceResult] AS [r] WHERE DATEPART(year, [r].[StartTime]) = 2019
```

## DATEDIFF Support
The SQL `DATEDIFF` function is supported for the following types:
* Instant (extension methods)
* OffsetDateTime (extension methods)
* LocalDateTime (extension methods)
* LocalDate (extension methods)
* LocalTime (extension methods)
* Duration (extension methods)

**Note**: Please add a using statement in order to use the extension methods:
```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
```

### Supported Parts
* Year
* Quarter
* Month
* DayOfYear
* Day
* Week
* WeekDay
* Hour
* Minute
* Second
* Millisecond
* Microsecond
* Nanosecond
* TzOffset
* IsoWeek

```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// DateDiff based on 'day'
DbFunctions dbFunctions = null;

await this.Db.Race
    .Where(r => dbFunctions.DateDiffDay(r.Date, new LocalDate(2020, 1, 1)) >= 200)
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[Date], [r].[ScheduledDuration], [r].[ScheduledStart], [r].[ScheduledStartTime]
// FROM [Race] AS [r]
// WHERE DATEDIFF(DAY, [r].[Date], '2020-01-01') >= 200
```

## DATEDIFF_BIG Support
The SQL `DATEDIFF_BIG` function is supported for the following types:
* Instant (extension methods)
* OffsetDateTime (extension methods)
* LocalDateTime (extension methods)
* LocalTime (extension methods)
* Duration (extension methods)

**Note**: Please add a using statement in order to use the extension methods:
```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
```

### Supported Parts
* Second
* Millisecond
* Microsecond
* Nanosecond

```csharp
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;

// DateDiffBig based on 'second'
DbFunctions dbFunctions = null;

await this.Db.RaceResult
    .Where(r => dbFunctions.DateDiffBigSecond(r.StartTime, Instant.FromUtc(2019, 7, 1, 0, 0)) >= 100000)
    .ToListAsync();

// Translates to: 
// SELECT [r].[Id], [r].[EndTime], [r].[OffsetFromWinner], [r].[StartTime], [r].[StartTimeOffset]
// FROM [RaceResult] AS [r]
// WHERE DATEDIFF_BIG(SECOND, [r].[StartTime], '2019-07-01T00:00:00.0000000Z') >= CAST(100000 AS bigint)
```
