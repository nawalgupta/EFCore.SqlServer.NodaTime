﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
using SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests.Models
{
    public class RacingContext : DbContext
    {
        readonly TestLoggerFactory _loggerFactory
            = new TestLoggerFactory();

        public DbSet<Race> Race { get; set; }

        public DbSet<RaceResult> RaceResult { get; set; }

        public string Sql
            => _loggerFactory.Logger.Sql;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseSqlServer(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NodaTimeTests",
                    x => x.UseNodaTime())
                .UseLoggerFactory(_loggerFactory);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Race>()
                .HasData(
                    new Race { Id = 1, Date = new LocalDate(2019, 1, 1), ScheduledStart = new LocalTime(8, 0, 0), ScheduledDuration = Duration.FromHours(1) },
                    new Race { Id = 2, Date = new LocalDate(2019, 2, 1), ScheduledStart = new LocalTime(9, 0, 0), ScheduledDuration = Duration.FromHours(2) },
                    new Race { Id = 3, Date = new LocalDate(2019, 3, 1), ScheduledStart = new LocalTime(10, 0, 0), ScheduledDuration = Duration.FromHours(3) },
                    new Race { Id = 4, Date = new LocalDate(2019, 4, 1), ScheduledStart = new LocalTime(11, 0, 0), ScheduledDuration = Duration.FromHours(4) },
                    new Race { Id = 5, Date = new LocalDate(2019, 5, 1), ScheduledStart = new LocalTime(12, 0, 0), ScheduledDuration = Duration.FromHours(5) },
                    new Race { Id = 6, Date = new LocalDate(2019, 6, 1), ScheduledStart = new LocalTime(13, 0, 0), ScheduledDuration = Duration.FromHours(6) },
                    new Race { Id = 7, Date = new LocalDate(2019, 7, 1), ScheduledStart = new LocalTime(14, 0, 0), ScheduledDuration = Duration.FromHours(7) },
                    new Race { Id = 8, Date = new LocalDate(2019, 8, 1), ScheduledStart = new LocalTime(15, 0, 0), ScheduledDuration = Duration.FromHours(8) },
                    new Race { Id = 9, Date = new LocalDate(2019, 9, 1), ScheduledStart = new LocalTime(16, 0, 0), ScheduledDuration = Duration.FromHours(9) },
                    new Race { Id = 10, Date = new LocalDate(2019, 10, 1), ScheduledStart = new LocalTime(17, 0, 0), ScheduledDuration = Duration.FromHours(10) },
                    new Race { Id = 11, Date = new LocalDate(2019, 11, 1), ScheduledStart = new LocalTime(18, 0, 0), ScheduledDuration = Duration.FromHours(11) },
                    new Race { Id = 12, Date = new LocalDate(2019, 12, 1), ScheduledStart = new LocalTime(19, 0, 0), ScheduledDuration = Duration.FromHours(12) }
                );


            modelBuilder.Entity<RaceResult>()
                .HasData(
                    new RaceResult { Id = 1, StartTime = Instant.FromUtc(2019, 1, 1, 8, 0), EndTime = Instant.FromUtc(2019, 1, 1, 9, 0) },
                    new RaceResult { Id = 2, StartTime = Instant.FromUtc(2019, 2, 1, 9, 0), EndTime = Instant.FromUtc(2019, 2, 1, 10, 0) },
                    new RaceResult { Id = 3, StartTime = Instant.FromUtc(2019, 3, 1, 10, 0), EndTime = Instant.FromUtc(2019, 3, 1, 11, 0) },
                    new RaceResult { Id = 4, StartTime = Instant.FromUtc(2019, 4, 1, 11, 0), EndTime = Instant.FromUtc(2019, 4, 1, 12, 0) },
                    new RaceResult { Id = 5, StartTime = Instant.FromUtc(2019, 5, 1, 12, 0), EndTime = Instant.FromUtc(2019, 5, 1, 13, 0) },
                    new RaceResult { Id = 6, StartTime = Instant.FromUtc(2019, 6, 1, 13, 0), EndTime = Instant.FromUtc(2019, 6, 1, 14, 0) },
                    new RaceResult { Id = 7, StartTime = Instant.FromUtc(2019, 7, 1, 14, 0), EndTime = Instant.FromUtc(2019, 7, 1, 15, 0) },
                    new RaceResult { Id = 8, StartTime = Instant.FromUtc(2019, 8, 1, 15, 0), EndTime = Instant.FromUtc(2019, 8, 1, 16, 0) },
                    new RaceResult { Id = 9, StartTime = Instant.FromUtc(2019, 9, 1, 16, 0), EndTime = Instant.FromUtc(2019, 9, 1, 17, 0) },
                    new RaceResult { Id = 10, StartTime = Instant.FromUtc(2019, 10, 1, 17, 0), EndTime = Instant.FromUtc(2019, 10, 1, 18, 0) },
                    new RaceResult { Id = 11, StartTime = Instant.FromUtc(2019, 11, 1, 18, 0), EndTime = Instant.FromUtc(2019, 11, 1, 19, 0) },
                    new RaceResult { Id = 12, StartTime = Instant.FromUtc(2019, 12, 1, 19, 0), EndTime = Instant.FromUtc(2019, 12, 1, 20, 0) });
        }
    }
}
