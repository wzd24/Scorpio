using JetBrains.Annotations;
using Scorpio.EntityFrameworkCore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public class ScorpioDbContextOptions
    {
        internal List<Action<DbContextConfigurationContext>> DefaultPreConfigureActions { get; set; }

        internal Action<DbContextConfigurationContext> DefaultConfigureAction { get; set; }

        internal Dictionary<Type, List<object>> PreConfigureActions { get; set; }

        internal Dictionary<Type, object> ConfigureActions { get; set; }

        internal IEnumerable<IModelCreatingContributor> ModelCreatingContributors => _modelCreatingContributors;

        private List<IModelCreatingContributor> _modelCreatingContributors;

        /// <summary>
        /// 
        /// </summary>
        public ScorpioDbContextOptions()
        {
            DefaultPreConfigureActions = new List<Action<DbContextConfigurationContext>>();
            PreConfigureActions = new Dictionary<Type, List<object>>();
            ConfigureActions = new Dictionary<Type, object>();
            _modelCreatingContributors = new List<IModelCreatingContributor>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void PreConfigure([NotNull] Action<DbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            DefaultPreConfigureActions.Add(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Configure([NotNull] Action<DbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));

            DefaultConfigureAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="action"></param>
        public void PreConfigure<TDbContext>([NotNull] Action<DbContextConfigurationContext<TDbContext>> action)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            Check.NotNull(action, nameof(action));

            var actions = PreConfigureActions.GetOrDefault(typeof(TDbContext));
            if (actions == null)
            {
                PreConfigureActions[typeof(TDbContext)] = actions = new List<object>();
            }

            actions.Add(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="action"></param>
        public void Configure<TDbContext>([NotNull] Action<DbContextConfigurationContext<TDbContext>> action)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            Check.NotNull(action, nameof(action));

            ConfigureActions[typeof(TDbContext)] = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelCreatingContributor"></param>
        public void AddModelCreatingContributor(IModelCreatingContributor modelCreatingContributor)
        {
            _modelCreatingContributors.AddIfNotContains(modelCreatingContributor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContributor"></typeparam>
        public void AddModelCreatingContributor<TContributor>()
            where TContributor:class,IModelCreatingContributor
        {
            AddModelCreatingContributor(Activator.CreateInstance<TContributor>());
        }
    }
}
