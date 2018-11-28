using Microsoft.EntityFrameworkCore;
using Scorpio.Data;
using Scorpio.EntityFrameworkCore;
using Scorpio.Uow;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Uow
{
    internal class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : ScorpioDbContext
    {
        private readonly IEfTransactionStrategy _transactionStrategy;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IConnectionStringResolver _connectionStringResolver;

        public UnitOfWorkDbContextProvider(
            IEfTransactionStrategy transactionStrategy,
            IServiceProvider serviceProvider,
           IUnitOfWorkManager unitOfWorkManager,
           IConnectionStringResolver connectionStringResolver)
        {
            _transactionStrategy = transactionStrategy;
            _serviceProvider = serviceProvider;
            _unitOfWorkManager = unitOfWorkManager;
            _connectionStringResolver = connectionStringResolver;
        }
        public TDbContext GetDbContext()
        {
            if (!(_unitOfWorkManager.Current is EfUnitOfWork uow))
            {
                throw new  NotSupportedException($"UnitOfWork is not type of {typeof(EfUnitOfWork).FullName}.");
            }
            var connectionString = _connectionStringResolver.Resolve<TDbContext>();
           return uow.GetOrCreateDbContext(connectionString, () => CreateDbContext(uow, connectionString));
        }

        private TDbContext CreateDbContext(IUnitOfWork uow, string connectionString)
        {
            var context = uow.Options.IsTransactional ?? true ?
                CreateDbContextWithTransactional(uow, connectionString) :
                _serviceProvider.GetService<TDbContext>();
            if (uow.Options.Timeout.HasValue && 
                context.Database.IsRelational() && 
                context.Database.GetCommandTimeout().HasValue)
            {
                context.Database.SetCommandTimeout(uow.Options.Timeout.Value);
            }
            return context;
        }

        private TDbContext CreateDbContextWithTransactional(IUnitOfWork uow, string connectionString)
        {
            return _transactionStrategy.CreateDbContext<TDbContext>(connectionString,uow.Options);
        }
    }
}
