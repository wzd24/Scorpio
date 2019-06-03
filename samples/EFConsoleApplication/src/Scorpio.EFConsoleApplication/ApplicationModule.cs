using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Scorpio.Data;
using Scorpio.EntityFrameworkCore;
using Scorpio.EntityFrameworkCore.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Scorpio.EFConsoleApplication
{
    [DependsOn(typeof(EntityFrameworkCoreModule))]
    [DependsOn(typeof(Application.ApplicationModule))]
    public sealed class ApplicationModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddScorpioDbContext<DemoDbContext>(opt=>
            {
                opt.AddSaveChangeHandler<DemoOnSaveChangeHandler>();
            });
            context.Services.Configure<ScorpioDbContextOptions>(o =>
            {
                o.Configure(c => c.UseSqlServer());
            });
            context.Services.Configure<DbConnectionOptions>(opt =>
            {
                opt.ConnectionStrings.Default = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=Demo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            });
            context.Services.Configure<DataFilterOptions>(opts =>
            {
                opts.Configure<ISoftDelete>(c => c.IsEnabled = true);
            });
            context.Services.AddLogging(l=>l.AddConsole());
            context.Services.RegisterAssemblyByConvention();
        }
    }

    class SoftDelete : ISoftDelete
    {
        public bool IsDeleted { get; set; } = false;
    }
}
