﻿using AspectCore.Injector;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Scorpio.Data;
using Scorpio.Guids;
using Scorpio.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public abstract class ScorpioDbContext<TDbContext> : ScorpioDbContext
        where TDbContext : ScorpioDbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextOptions"></param>
        /// <param name="filterOptions"></param>
        /// <param name="serviceProvider"></param>
        protected ScorpioDbContext(IServiceProvider serviceProvider, DbContextOptions<TDbContext> contextOptions, IOptions<DataFilterOptions> filterOptions) : base(serviceProvider, contextOptions, filterOptions)
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class ScorpioDbContext : DbContext
    {
        private readonly DataFilterOptions _filterOptions;

        /// <summary>
        /// 
        /// </summary>
        [FromContainer]
        public IGuidGenerator GuidGenerator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [FromContainer]
        public IOnSaveChangeHandlersFactory OnSaveChangeHandlersFactory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IDataFilter DataFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ScorpioDbContextOptions ScorpioDbContextOptions { get; }

        /// <summary>
        /// 
        /// </summary>
        [FromContainer]
        public ILogger<ScorpioDbContext> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        private static readonly MethodInfo _configureGlobalFiltersMethodInfo
    = typeof(ScorpioDbContext)
        .GetMethod(
            nameof(ConfigureGlobalFilters),
            BindingFlags.Instance | BindingFlags.NonPublic
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextOptions"></param>
        /// <param name="filterOptions"></param>
        /// <param name="serviceProvider"></param>
        protected ScorpioDbContext(IServiceProvider serviceProvider, DbContextOptions contextOptions, IOptions<DataFilterOptions> filterOptions)
            : base(contextOptions)
        {

            _filterOptions = filterOptions.Value;
            ServiceProvider = serviceProvider;
            DataFilter = ServiceProvider.GetService<IDataFilter>();
            ScorpioDbContextOptions = ServiceProvider.GetRequiredService<IOptions<ScorpioDbContextOptions>>().Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var context = new ModelCreatingContributionContext(modelBuilder);
            foreach (var item in ScorpioDbContextOptions.ModelCreatingContributors)
            {
                item.Contributor(context);
            }
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                _configureGlobalFiltersMethodInfo
                   .MakeGenericMethod(entityType.ClrType)
                   .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var entityChangeList = ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();
            var saveChangeHandlers = OnSaveChangeHandlersFactory.CreateHandlers();
            if (entityChangeList.Count > 0)
            {
                saveChangeHandlers.ForEach(handler => AsyncHelper.RunSync(() => handler.PreSaveChangeAsync(entityChangeList)));
            }
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            if (entityChangeList.Count > 0)
            {
                saveChangeHandlers.ForEach(handler => AsyncHelper.RunSync(() => handler.PostSaveChangeAsync(entityChangeList)));
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityChangeList = ChangeTracker.Entries().ToList();
            var saveChangeHandlers = OnSaveChangeHandlersFactory.CreateHandlers();
            if (entityChangeList.Count > 0)
            {
                await saveChangeHandlers.ForEachAsync(async handler => await handler.PreSaveChangeAsync(entityChangeList));
            }
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            if (entityChangeList.Count > 0)
            {
                await saveChangeHandlers.ForEachAsync(async handler => await handler.PostSaveChangeAsync(entityChangeList));
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="entityType"></param>
        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>(entityType))
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityType"></param>
        /// <returns></returns>
        protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
        {
            return _filterOptions.Descriptors.Keys.Any(t => t.IsAssignableFrom(typeof(TEntity)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;
            _filterOptions.Descriptors.ForEach(item =>
            {
                if (item.Key.IsAssignableFrom(typeof(TEntity)))
                {
                    var filterexpression = item.Value.BuildFilterExpression<TEntity>();
                    filterexpression = filterexpression.Or(filterexpression.Equal(expr2 => DataFilter.IsEnabled(item.Key)));
                    expression = expression == null ? filterexpression : expression.And(filterexpression);
                }
            });
            return expression;
        }
    }
}
