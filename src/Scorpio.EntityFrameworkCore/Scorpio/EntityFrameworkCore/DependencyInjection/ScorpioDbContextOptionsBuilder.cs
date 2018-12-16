using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Domain.Entities;
using Scorpio.Domain.Repositories;
using Scorpio.Domain.Repositories.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.EntityFrameworkCore.DependencyInjection
{
    internal class ScorpioDbContextOptionsBuilder<TDbContext> : IScorpioDbContextOptionsBuilder<TDbContext>
        where TDbContext : ScorpioDbContext<TDbContext>
    {

        public List<Action<DbContextOptionsBuilder<TDbContext>>> OptionsActions { get; }

        public Type DefaultRepositoryType { get; set; } = typeof(EfCoreRepository<,,>);

        public ITypeDictionary<IEntity, IRepository> RepositoryTypes { get; }
        public IServiceCollection Services { get; }

        public ScorpioDbContextOptionsBuilder(IServiceCollection services)
        {
            OptionsActions = new List<Action<DbContextOptionsBuilder<TDbContext>>>();
            RepositoryTypes = new TypeDictionary<IEntity, IRepository>();
            Services = services;
        }

        public void AddRepository(Type entityType,Type repositoryType)
        {
            if (!entityType.IsAssignableTo(typeof(IEntity<>)))
            {
                throw new ScorpioException($"Given entityType is not an entity: {entityType.AssemblyQualifiedName}. It must implement {typeof(IEntity<>).AssemblyQualifiedName}.");
            }
            if (!repositoryType.IsAssignableTo(typeof(IRepository<>)))
            {
                throw new ScorpioException($"Given repositoryType is not a repository: {entityType.AssemblyQualifiedName}. It must implement {typeof(IEfCoreRepository<>).AssemblyQualifiedName}.");
            }
            RepositoryTypes.Add(entityType,repositoryType);
        }

        public void AddRepository<TEntity,TRepository>()
            where TEntity:class,IEntity
            where TRepository:IRepository<TEntity>
        {
            RepositoryTypes.Add<TEntity, TRepository>();
        }
    }
}
