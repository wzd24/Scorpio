using Scorpio.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Uow
{
    class UnitOfWorkTransactionStrategy : IEfTransactionStrategy
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public TDbContext CreateDbContext<TDbContext>(string connectionString, UnitOfWorkOptions options) where TDbContext : ScorpioDbContext
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void InitOptions(UnitOfWorkOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
