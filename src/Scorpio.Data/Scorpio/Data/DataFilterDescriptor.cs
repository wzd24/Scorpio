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
        internal Expression<Func<TEntity, bool>> BuildFilterExpression<TEntity>(IDataFilter dataFilter,IFilterContext context) where TEntity : class
        {
            var filterexpression = BuildFilterExpression<TEntity>(context);
            var expression = filterexpression.Or(filterexpression.Equal(expr2 => dataFilter.IsEnabled(FilterType)));
            return expression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        internal Expression<Func<TEntity, bool>> BuildFilterExpression<TEntity>(IFilterContext context) where TEntity : class
        {
            var filterexpression = BuildFilterExpressionCore<TEntity>(context);
            return filterexpression;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected abstract Expression<Func<TEntity, bool>> BuildFilterExpressionCore<TEntity>(IFilterContext context) where TEntity : class;
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
    internal sealed class SoftDeleteDataFilterDescriptor : DataFilterDescriptor<ISoftDelete>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        protected override Expression<Func<TEntity, bool>> BuildFilterExpressionCore<TEntity>(IFilterContext context)
        {
            return e => ((ISoftDelete)e).IsDeleted== false;
            //return GetExpression(context.GetPropertyExpression<TEntity,bool>("IsDeleted"));
        }

        //private Expression<Func<TEntity,bool>> GetExpression<TEntity,TProperty>(Expression<Func<TEntity,TProperty>> expression)
        //{
        //    var right = Expression.Constant(false);
        //    return Expression.Lambda<Func<TEntity,bool>>( Expression.Equal(expression.Body, right),expression.Parameters[0]);
        //}
    }
}
