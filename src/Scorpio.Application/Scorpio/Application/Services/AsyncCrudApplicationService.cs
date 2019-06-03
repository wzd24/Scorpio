﻿using Scorpio.Application.Dtos;
using Scorpio.Domain.Entities;
using Scorpio.Domain.Repositories;
using System;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Scorpio.Linq;
using AspectCore.Injector;

namespace Scorpio.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AsyncCrudApplicationService<TEntity, TEntityDto, TKey>
        : AsyncCrudApplicationService<TEntity, TEntityDto, TKey, ListRequest<TEntityDto>>,
        IAsyncCrudApplicationService<TEntityDto, TKey>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected AsyncCrudApplicationService(IServiceProvider serviceProvider, IRepository<TEntity, TKey> repository) : base(serviceProvider, repository)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    public abstract class AsyncCrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput>
        : AsyncCrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto>,
        IAsyncCrudApplicationService<TEntityDto, TKey, TGetListInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected AsyncCrudApplicationService(IServiceProvider serviceProvider, IRepository<TEntity, TKey> repository) : base(serviceProvider, repository)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TGetListInput"></typeparam>
    /// <typeparam name="TCreateInput"></typeparam>
    public abstract class AsyncCrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        : AsyncCrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>,
        IAsyncCrudApplicationService<TEntityDto, TKey, TGetListInput, TCreateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        protected AsyncCrudApplicationService(IServiceProvider serviceProvider, IRepository<TEntity, TKey> repository) : base(serviceProvider, repository)
        {
        }
    }
    public abstract class AsyncCrudApplicationService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : CrudApplicationServiceBase<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>,
        IAsyncCrudApplicationService<TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {
        [FromContainer]
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        protected AsyncCrudApplicationService(IServiceProvider serviceProvider, IRepository<TEntity, TKey> repository) : base(serviceProvider, repository)
        {
        }

        public virtual async Task<TEntityDto> CreateAsync(TCreateInput input, CancellationToken cancellationToken = default)
        {
            var entity = Mapper.Map<TEntity>(input);
            await Repository.InsertAsync(entity, cancellationToken: cancellationToken);
            return Mapper.Map<TEntityDto>(entity);
        }


        public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            await Repository.DeleteAsync(id, cancellationToken: cancellationToken);
        }

        public virtual TEntityDto Get(TKey id)
        {
            return Mapper.Map<TEntityDto>(Repository.Get(id));
        }

        public async Task<TEntityDto> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return Mapper.Map<TEntityDto>(await Repository.GetAsync(id, cancellationToken: cancellationToken));
        }


        public async Task<IPagedResult<TEntityDto>> GetListAsync(TGetListInput input, CancellationToken cancellationToken = default)
        {
            var query = Repository.AsQueryable();
            query = ApplyFilter(query, input);
            var totalCount = await AsyncQueryableExecuter.CountAsync(query, cancellationToken);
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);
            return new PagedResult<TEntityDto>(await AsyncQueryableExecuter.ToListAsync(query.ProjectTo<TEntityDto>(Configuration), cancellationToken)) { TotalCount = totalCount };
        }

        public async Task<TEntityDto> UpdateAsync(TKey id, TUpdateInput input, CancellationToken cancellationToken = default)
        {
            var entity = Repository.Get(id);
            Mapper.Map(input, entity);
            await Repository.UpdateAsync(entity, cancellationToken: cancellationToken);
            return Mapper.Map<TEntityDto>(entity);
        }
    }
}
