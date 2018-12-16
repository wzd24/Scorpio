using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scorpio.Data;

namespace Scorpio.EFConsoleApplication
{
    internal class DemoDbContext : EntityFrameworkCore.ScorpioDbContext<DemoDbContext>
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> contextOptions, IOptions<DataFilterOptions> filterOptions) : base(contextOptions, filterOptions)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

    }
}
