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
        public Expression FilterExpression { get; set; }

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
        internal protected virtual Expression<Func<TEntity, bool>> BuildFilterExpression<TEntity>() where TEntity : class
        {
            return e => e == null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFilter"></typeparam>
    public abstract class DataFilterDescriptor<TFilter> : DataFilterDescriptor
    {
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
        protected internal override Expression<Func<TEntity, bool>> BuildFilterExpression<TEntity>()
        {
            return e => ((ISoftDelete)e).IsDeleted == false;
        }
    }
}
