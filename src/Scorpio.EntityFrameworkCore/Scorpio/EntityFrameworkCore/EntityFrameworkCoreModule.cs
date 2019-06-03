using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Domain;
using Scorpio.Modularity;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Scorpio.Uow;
using Scorpio.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Scorpio.Linq;
using Scorpio.Guids;
using Scorpio.EntityFrameworkCore.DependencyInjection;
namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(DomainModule))]
    [DependsOn(typeof(DataModule))]
    [DependsOn(typeof(GuidsModule))]
    public sealed class EntityFrameworkCoreModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<ScorpioDbContextOptions>(options =>
            {
                options.AddModelCreatingContributor<DataModelCreatingContributor>();
                options.PreConfigure(dbConfigContext => dbConfigContext.DbContextOptions.ConfigureWarnings(
                    warnings => warnings.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning)
                    ));
            });
            context.Services.Configure<DbConnectionOptions>(context.Configuration);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.ScorpioDbContext(builder =>
            {
                builder.AddSaveChangeHandler<SoftDeleteSaveChangeHandler>();
            });
            context.Services.ReplaceTransient<IUnitOfWork, EfUnitOfWork>();
            context.Services.AddTransient<IOnSaveChangeHandlersFactory, OnSaveChangeHandlersFactory>();
            context.Services.AddTransient<IEfTransactionStrategy, UnitOfWorkEfTransactionStrategy>();
            context.Services.AddTransient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>));
            context.Services.ReplaceSingleton<IAsyncQueryableExecuter, EntityFrameworkCoreAsyncQueryableExecuter>();
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
