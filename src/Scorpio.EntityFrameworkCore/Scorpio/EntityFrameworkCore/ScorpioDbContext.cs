using AspectCore.Injector;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Scorpio.Data;
using Scorpio.Guids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

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
        protected ScorpioDbContext(DbContextOptions<TDbContext> contextOptions, DataFilterOptions filterOptions) : base(contextOptions, filterOptions)
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
        public IDataFilter DataFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [FromContainer]
        public ILogger<ScorpioDbContext> Logger { get; set; }

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
        protected ScorpioDbContext(DbContextOptions contextOptions, DataFilterOptions filterOptions)
            : base(contextOptions)
        {
            _filterOptions = filterOptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                    var filterexpression = item.Value.FilterExpression as Expression<Func<TEntity, bool>>;
                    filterexpression = filterexpression.Or(filterexpression.Equal(expr2 => DataFilter.IsEnabled(item.Key)));
                    expression = expression == null ? filterexpression : expression.And(filterexpression);
                }
            });
            return expression;
        }
    }
}
