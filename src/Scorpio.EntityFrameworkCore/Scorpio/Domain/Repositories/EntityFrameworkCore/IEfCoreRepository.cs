﻿using Microsoft.EntityFrameworkCore;

using Scorpio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Domain.Repositories.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
  public  interface IEfCoreRepository<TEntity>:IRepository<TEntity> 
        where TEntity:class, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        DbContext DbContext { get; }


    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IEfCoreRepository<TEntity, TKey> : IEfCoreRepository<TEntity>, IRepository<TEntity, TKey>
        where TEntity:class,IEntity<TKey>
    {

    }
}
