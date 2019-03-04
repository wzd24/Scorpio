﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Scorpio.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Scorpio.Uow
{
    internal class TransactionDescriptor:IDisposable
    {
        private readonly HashSet<DbContext> _dbContexts=new HashSet<DbContext>();

        public IDbContextTransaction Transaction { get; }

        public IEnumerable<DbContext> DbContexts => _dbContexts.ToImmutableList();

        public TransactionDescriptor(IDbContextTransaction transaction)
        {
            Transaction = transaction;
        }

        public void AddContext(DbContext context)
        {
            _dbContexts.Add(context);
        }

        public void Commit()
        {
            Transaction.Commit();
            _dbContexts.ForEach(context =>
            {
                if (!context.HasRelationalTransactionManager())
                {
                    context.Database.CommitTransaction();
                }
            });
        }

        #region IDisposable Support
        private bool _disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Transaction.Dispose();
                    _dbContexts.ForEach(context => context.Dispose());
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                _disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~TransactionDescriptor() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
