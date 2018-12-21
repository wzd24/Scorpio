﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scorpio.Data;

namespace Scorpio.EFConsoleApplication
{
    internal class DemoDbContext : EntityFrameworkCore.ScorpioDbContext<DemoDbContext>
    {
        public DemoDbContext(IServiceProvider serviceProvider, DbContextOptions<DemoDbContext> contextOptions, IOptions<DataFilterOptions> filterOptions) : base(serviceProvider, contextOptions, filterOptions)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }

    }
}
