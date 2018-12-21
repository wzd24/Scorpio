using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DataFilterDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Type FilterType { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterType"></param>
        internal DataFilterDescriptor(Type filterType)
        {
            FilterType = filterType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataFilterState GetState()
        {
            return new DataFilterState(IsEnabled);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        internal Expression<Func<TEntity, bool>> BuildFilterExpression<TEntity>(IDataFilter dataFilter) where TEntity : class
        {
            var filterexpression = BuildFilterExpression<TEntity>();
            var expression = filterexpression.Or(filterexpression.Equal(expr2 => dataFilter.IsEnabled(FilterType)));
            return expression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        internal Expression<Func<TEntity, bool>> BuildFilterExpression<TEntity>() where TEntity : class
        {
            var filterexpression = BuildFilterExpressionCore<TEntity>();
            return filterexpression;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected abstract Expression<Func<TEntity, bool>> BuildFilterExpressionCore<TEntity>() where TEntity : class;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    public abstract class DataFilterDescriptor<TFilter> : DataFilterDescriptor
    {
        /// <summary>
        /// 
        /// </summary>
        protected DataFilterDescriptor() : base(typeof(TFilter))
        {
            IsEnabled = true;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public sealed class SoftDeleteDataFilterDescriptor : DataFilterDescriptor<ISoftDelete>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected override Expression<Func<TEntity, bool>> BuildFilterExpressionCore<TEntity>()
        {
            return e => ((ISoftDelete)e).IsDeleted == false;
        }
    }
}
