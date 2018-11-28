using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scorpio.EntityFrameworkCore;

namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public class EfUnitOfWork : UnitOfWorkBase
    {
        private readonly IEfTransactionStrategy _transactionStrategy;

        /// <summary>
        /// 
        /// </summary>
        protected IDictionary<string, ScorpioDbContext> ActiveDbContexts { get; } = new Dictionary<string, ScorpioDbContext>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="transactionStrategy"></param>
        /// <param name="options"></param>
        public EfUnitOfWork(IServiceProvider serviceProvider, IEfTransactionStrategy transactionStrategy, IOptions<UnitOfWorkDefaultOptions> options) : base(serviceProvider, options)
        {
            _transactionStrategy = transactionStrategy;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SaveChanges()
        {
            foreach (var item in GetAllActiveDbContexts())
            {
                item.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAsync()
        {
            foreach (var item in GetAllActiveDbContexts())
            {
                await item.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void BeginUow()
        {
            if (Options.IsTransactional==true)
            {
                _transactionStrategy.InitOptions(Options);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CompleteUow()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Task CompleteUowAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void DisposeUow()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取全部活动的DBContext
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ScorpioDbContext> GetAllActiveDbContexts()
        {
            return ActiveDbContexts.Values.ToImmutableList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public virtual TDbContext GetOrCreateDbContext<TDbContext>(string connectionString, Func<TDbContext> factory)
            where TDbContext : ScorpioDbContext
        {
            var connectionKey = $"DbContext_{typeof(TDbContext).FullName}_{connectionString}";
            return ActiveDbContexts.GetOrAdd(connectionKey, factory) as TDbContext;
        }
    }
}
