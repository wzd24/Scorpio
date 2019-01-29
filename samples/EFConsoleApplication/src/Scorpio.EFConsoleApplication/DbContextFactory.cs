using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.EFConsoleApplication
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
    {
        public DemoDbContext CreateDbContext(string[] args)
        {
            var bootstrapper = Bootstrapper.Create<ApplicationModule>();
            bootstrapper.Initialize();
            return bootstrapper.ServiceProvider.GetService<DemoDbContext>();
        }
    }
}
