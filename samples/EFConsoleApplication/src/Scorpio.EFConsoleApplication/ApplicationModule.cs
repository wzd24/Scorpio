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
namespace Scorpio.EFConsoleApplication
{
    [DependsOn(typeof(EntityFrameworkCoreModule))]
    public sealed class ApplicationModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddScorpioDbContext<DemoDbContext>(b =>
            {
            });
            context.Services.Configure<ScorpioDbContextOptions>(o =>
            {
                o.Configure(c => c.UseSqlServer());
            });
            context.Services.AddSaveChangeHandler<DemoOnSaveChangeHandler>();
            context.Services.Configure<DbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Demo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            });
            context.Services.Configure<DataFilterOptions>(options =>
            {
            //options.ConfigureFilter<ISoftDelete>(c => c.Disable());
            });
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
