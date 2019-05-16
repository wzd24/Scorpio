﻿using Microsoft.EntityFrameworkCore;
using Scorpio.Domain.Entities;
using Scorpio.Domain.Repositories.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public static class EfCoreRepositoryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static DbContext GetDbContext<TEntity, TKey>(this IBasicRepository<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey>
        {
            return repository.ToEfCoreRepository().DbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static IEfCoreRepository<TEntity, TKey> ToEfCoreRepository<TEntity, TKey>(this IBasicRepository<TEntity, TKey> repository)
            where TEntity : class, IEntity<TKey>
        {
            if (!(repository is IEfCoreRepository<TEntity, TKey> efCoreRepository))
            {
                throw new ArgumentException("Given repository does not implement " + typeof(IEfCoreRepository<TEntity, TKey>).AssemblyQualifiedName, nameof(repository));
            }

            return efCoreRepository;
        }
    }
}
